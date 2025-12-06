using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finisher.Application.Configurations;

public static class AppCdn
{
    public static void ConfigureAppCdn(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingConfigure = configuration.GetSection("App");
        services.Configure<AppSetting>(appSettingConfigure);

        var appSettings = configuration.GetSection("App").Get<AppSetting>();

        CdnBaseUrl = appSettings?.CdnBaseUrl.TrimEnd('/') ?? string.Empty;
    }

    public static string CdnBaseUrl { get; private set; } = string.Empty;
}

public sealed class AppSetting
{
    public required string CdnBaseUrl { get; init; }
}
