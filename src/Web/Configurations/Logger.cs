using System.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;

namespace Finisher.Web.Configurations;

public static class Logger
{
    public static void ConfigureLogger(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.AddLogging(config =>
        {
            config.ClearProviders();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.With<OpenTelemetryEnricher>()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.Hosting.Lifetime"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Hosting"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Hosting.Diagnostics"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted"))
                .CreateLogger();

            config.AddSerilog(logger);
        });
    }

    public static void ConfigureUiLogger(this IServiceCollection builder, IConfiguration configuration)
    {
        builder.Configure<UiLogger>(configuration.GetSection("UiLogger"));
    }
}

public sealed class UiLogger
{
    public required bool IsEnabled { get; init; }
}

public class OpenTelemetryEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var activity = Activity.Current;
        if (activity != null)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("trace.id", activity.TraceId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("parent.id", activity.ParentSpanId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("transaction.id", activity.SpanId));

            if (logEvent.Exception != null)
            {
                activity.SetStatus(ActivityStatusCode.Error, logEvent.Exception.Message);
                activity.AddException(logEvent.Exception);
            }
        }
    }
}
