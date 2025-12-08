namespace Finisher.Web.Configurations;

internal static class Configuration
{
    public static IConfigurationRoot ConfigureConfiguration(this WebApplicationBuilder builder)
    {
        var builderConfiguration = builder.Configuration.AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", reloadOnChange: true, optional: true)
            .AddUserSecrets<ConfigurationAssembly>()
            .AddEnvironmentVariables();

        return builderConfiguration.Build();
    }
}

internal abstract class ConfigurationAssembly;
