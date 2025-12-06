namespace Finisher.Web.Endpoints;

internal abstract class BaseDeleteEndpoint<TRequest>(IMessageBus bus, string apiName, string idType = ApiRoutes.IdRoute) : Endpoint<TRequest>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Delete($"{Api.AddSeparator()}" + idType);
        Tags(ApiTag.Delete);
        Roles(UserRoles.GetRolesForDelete());
        Policies(UserPolicies.GetPoliciesForDelete());
        Permissions(UserPermissions.GetPermissionsForDelete());
        Summary(s => s.Summary = SummaryCreator.Delete(Api));
    }
}
