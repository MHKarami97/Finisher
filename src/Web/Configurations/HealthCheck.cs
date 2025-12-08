using Finisher.Infrastructure.Data;

namespace Finisher.Web.Configurations;

internal static class HealthCheck
{
    public static void ConfigureHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
    }

    public static void ConfigureHealthCheck(this WebApplication app)
    {
        app.UseHealthChecks("/health");
    }
}
