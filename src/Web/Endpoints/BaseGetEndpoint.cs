using System.Globalization;

namespace Finisher.Web.Endpoints;

internal abstract class BaseGetEndpoint<TRequest, TResponse>(IMessageBus bus, string apiName, string idType = ApiRoutes.IdRoute) : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Get($"{Api.AddSeparator()}" + idType);
        Tags(ApiTag.Get);
        AllowAnonymous();
        ResponseCache(int.Parse(Config["Cache:DurationOnSecond"]!, CultureInfo.InvariantCulture));
        Roles(UserRoles.GetRolesForView());
        Policies(UserPolicies.GetPoliciesForView());
        Permissions(UserPermissions.GetPermissionsForView());
        Summary(s => s.Summary = SummaryCreator.GetById(Api));
    }
}
