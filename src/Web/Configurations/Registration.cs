using Finisher.Application;
using Finisher.Infrastructure;

namespace Finisher.Web.Configurations;

internal static class Registration
{
    public static void ConfigureRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddApplicationServices(configuration);
        builder.AddDomainServices();
        builder.AddInfrastructureServices();
    }
}
