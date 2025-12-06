using Finisher.Application;
using Finisher.Infrastructure;

namespace Finisher.Web.Configurations;

public static class Registration
{
    public static void ConfigureRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddApplicationServices(configuration);
        builder.AddDomainServices();
        builder.AddInfrastructureServices();
    }
}
