using Finisher.Application.Interfaces.User;
using Finisher.Web.Services;

namespace Finisher.Web.Configurations;

internal static class ServiceCollection
{
    public static void ConfigureServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();
    }
}
