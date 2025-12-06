namespace Finisher.Web.Endpoints;

public abstract class BaseUpdateEndpoint<TRequest>(IMessageBus bus, string apiName, string idType = ApiRoutes.IdRoute) : Endpoint<TRequest>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Put($"{Api.AddSeparator()}" + idType);
        Tags(ApiTag.Update);
        Roles(UserRoles.GetRolesForUpdate());
        Policies(UserPolicies.GetPoliciesForUpdate());
        Permissions(UserPermissions.GetPermissionsForUpdate());
        Summary(s => s.Summary = SummaryCreator.Update(Api));
    }
}
