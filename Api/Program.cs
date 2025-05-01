using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Api.Controllers;
using Api.Handlers;
using Application.Abstracts;
using Application.Services;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Options;
using Infrastructure.Processors;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddOpenApi();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", options =>
    {
        options.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);  
            //.AllowAnyOrigin();
        //.WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<FitTrackDbContext>();

builder.Services.AddDbContext<FitTrackDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions
            .ConfigureDataSource(dataSourceBuilder => dataSourceBuilder.EnableDynamicJson())));

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IIndividualTrainingRepository, IndividualTrainingRepository>();
builder.Services.AddScoped<ISetRepository, SetRepository>();
//builder.Services.AddScoped(typeof(Controller<>));

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