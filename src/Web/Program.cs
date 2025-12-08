using System.Globalization;
using Finisher.Infrastructure.Data;
using Finisher.Web.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(formatProvider: new CultureInfo(Culture.DefaultCulture))
        .CreateBootstrapLogger();

    Log.Information("Starting up... {Now}", DateTimeOffset.UtcNow);

    Culture.ConfigureCulture();

    var configuration = builder.ConfigureConfiguration();
    var services = builder.Services;

    builder.ConfigureRegistration(configuration);
    builder.ConfigureKestrel(configuration);

    services.ConfigureServiceCollection();
    services.ConfigureHealthCheck();
    services.ConfigureSwaggerAuth(configuration);
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

    app.UseStaticFiles();
    app.ConfigureResponseCompression();
    app.ConfigureMiddleware();
    app.ConfigureCors(configuration);
    app.ConfigureHealthCheck();
    app.ConfigureRateLimit();
    app.UseHttpsRedirection();
    app.UseExceptionHandler(_ => { });
    app.ConfigureAuthentication();
    app.ConfigureAuthorization();
    app.ConfigureSwaggerAuth();
    app.ConfigureEndPoint();
    app.ConfigureOwasp(configuration);

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
