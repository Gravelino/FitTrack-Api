using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Api.Extensions;

public static class IdentityExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<FitTrackDbContext>();
    }
}
