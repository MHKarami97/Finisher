namespace Finisher.Web.Configurations;

internal static class Cors
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var setting = configuration
            .GetSection("Cors")
            .Get<CorsSetting>()!;

        services.AddCors(options => options
            .AddPolicy(setting.Name,
                policy => policy
                    .WithOrigins(setting.Origins)
                    .WithMethods(setting.Methods)
                    .WithHeaders(setting.Headers)
                    .AllowCredentials()
            ));
    }

    public static void ConfigureCors(this WebApplication app, IConfiguration configuration)
    {
        var setting = configuration
            .GetSection("Cors")
            .Get<CorsSetting>()!;

        app.UseCors(setting.Name);
    }

    internal sealed class CorsSetting
    {
        public required string Name { get; init; }
        public string[] Origins { get; init; } = [];
        public string[] Methods { get; init; } = [];
        public string[] Headers { get; init; } = [];
    }
}
