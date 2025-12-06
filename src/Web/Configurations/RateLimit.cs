using AspNetCoreRateLimit;

namespace Finisher.Web.Configurations;

internal static class RateLimit
{
    public static void ConfigureRateLimit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }

    public static void ConfigureRateLimit(this WebApplication app)
    {
        app.UseIpRateLimiting();
    }
}
