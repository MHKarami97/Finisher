using Microsoft.AspNetCore.ResponseCompression;

namespace Finisher.Web.Configurations;

internal static class ResponseCompression
{
    public static void ConfigureResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(o =>
        {
            o.Providers.Add<GzipCompressionProvider>();
            o.EnableForHttps = true;
            o.MimeTypes = ["text/plain", "text/css", "application/json", "application/javascript", "text/html"];
        });

        services.Configure<GzipCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.Optimal);
    }

    public static void ConfigureResponseCompression(this WebApplication app)
    {
        app.UseResponseCompression();
    }
}
