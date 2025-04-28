using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync<TContext>(
        this IHost host,
        CancellationToken cancellationToken = default) where TContext : DbContext
    {
        using IServiceScope scope = host.Services.CreateScope();
        ILogger<TContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        try
        {
            TContext context = scope.ServiceProvider.GetRequiredService<TContext>();
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Database migrations applied successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations");
            throw;
        }
    }
}