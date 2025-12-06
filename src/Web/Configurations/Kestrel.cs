namespace Finisher.Web.Configurations;

public static class Kestrel
{
    public static void ConfigureKestrel(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var setting = configuration
            .GetSection("Kestrel")
            .Get<KestrelSetting>()!;

        builder.WebHost.ConfigureKestrel(o =>
        {
            o.Limits.MaxRequestBodySize = setting.MaxRequestBodySize;
            o.AddServerHeader = false;
        });
    }

    public sealed class KestrelSetting
    {
        public required bool AddServerHeader { get; init; }
        public required long MaxRequestBodySize { get; init; }
    }
}
