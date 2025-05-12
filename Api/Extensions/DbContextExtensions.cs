using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FitTrackDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => npgsqlOptions.ConfigureDataSource(dataSourceBuilder => dataSourceBuilder.EnableDynamicJson())
            ));

        return services;
    }
}