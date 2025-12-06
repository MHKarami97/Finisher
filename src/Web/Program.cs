using System.Globalization;
using Finisher.Infrastructure.Data;
using Finisher.Web;
using Finisher.Web.Configurations;
using OwaspHeaders.Core.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: new CultureInfo("fa-IR"))
    .CreateBootstrapLogger();

Log.Information("Starting up... {Now}", DateTimeOffset.Now);

try
{
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fa-IR");
    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fa-IR");

    var builderConfiguration = builder.Configuration.AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", reloadOnChange: true, optional: true)
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();

    var configuration = builderConfiguration.Build();
    var services = builder.Services;

    builder.AddKeyVaultIfConfigured();
    builder.ConfigureRegistration(configuration);
    builder.AddWebServices();
    builder.ConfigureKestrel(configuration);

    services.ConfigureResponseCompression();
    services.ConfigureCors(configuration);
    services.ConfigureRateLimit(configuration);
    services.ConfigureLogger(configuration);
    services.ConfigureUiLogger(configuration);
    services.ConfigureAuthentication(configuration);
    services.ConfigureAuthorization();
    services.ConfigureEndPoint();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        await app.InitialiseDatabaseAsync();
    }
    else
    {
        app.UseHsts();
    }

    app.ConfigureResponseCompression();
    app.UseStaticFiles();
    app.ConfigureMiddleware();
    app.ConfigureCors(configuration);
    app.UseHealthChecks("/health");
    app.ConfigureRateLimit();
    app.UseHttpsRedirection();
    app.UseExceptionHandler(_ => { });
    app.ConfigureAuthentication();
    app.ConfigureAuthorization();
    app.ConfigureEndPoint();
    app.Map("/", () => Results.Redirect($"/{ApiRoutes.Api}"));
    app.UseSecureHeadersMiddleware(SecureHeaders.SecureHeadersConfiguration(configuration));

    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "An unhandled exception occured during bootstrapping");
    throw;
}
finally
{
    Log.Information("Stopped...");

    await Log.CloseAndFlushAsync();
}

namespace Finisher.Web
{
    public partial class Program
    {
    }
}
