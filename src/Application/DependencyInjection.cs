using System.Globalization;
using System.Reflection;
using Finisher.Application.Configurations;
using JasperFx.CodeGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine.FluentValidation;

namespace Finisher.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.ConfigureAppCdn(configuration);

        builder.Services.AddEasyCaching(options =>
            options.UseInMemory(configuration, MainConsts.CacheProviderName));

        builder.UseWolverine(o =>
        {
            o.Durability.Mode = DurabilityMode.MediatorOnly;
            o.UseFluentValidation();
            o.Discovery.IncludeAssembly(Assembly.GetExecutingAssembly());
            o.DefaultExecutionTimeout = TimeSpan.FromMinutes(int.Parse(configuration["Cache:HandlerTimeOutOnMinute"]!, CultureInfo.InvariantCulture));
            o.CodeGeneration.TypeLoadMode = builder.Environment.IsDevelopment() ? TypeLoadMode.Dynamic : TypeLoadMode.Static;
        });
    }

    public static void AddDomainServices(this IHostApplicationBuilder builder)
    {

    }
}
