using System.Security.Claims;
using System.Text;
using Api.Extensions;
using Api.Handlers;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

builder.Services.AddCustomCors();
builder.Services.AddCustomIdentity(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddAwsServices(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(options =>
{
    var clientId = builder.Configuration["Authentication:Google:ClientId"];
    if (clientId is null)
    {
        throw new ArgumentNullException(nameof(clientId));
    }
    
    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    if (clientSecret is null)
    {
        throw new ArgumentNullException(nameof(clientSecret));
    }
    
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
    options.ClaimActions.MapJsonKey("picture", "picture", "url");
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    
    options.Events.OnCreatingTicket = context =>
    {
        var picture = context.User.GetProperty("picture").GetString();

        if (!string.IsNullOrEmpty(picture))
        {
            ((ClaimsIdentity)context.Principal.Identity).AddClaim(
                new Claim("picture", picture)
            );
        }

        return Task.CompletedTask;
    };
}).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection(JwtOptions.JwtOptionsKey)
        .Get<JwtOptions>() ?? throw new ArgumentException(nameof(JwtOptions));

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["ACCESS_TOKEN"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionsHandler>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

await app.ApplyMigrationsAsync<FitTrackDbContext>();

app.UseCors("CorsPolicy");

//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("FitTrack API");
    });
//}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();