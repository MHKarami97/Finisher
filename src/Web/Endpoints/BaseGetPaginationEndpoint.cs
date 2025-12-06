namespace Finisher.Web.Endpoints;

public abstract class BaseGetPaginationEndpoint<TRequest, TResponse>(IMessageBus bus, string apiName) : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Get(Api.AddPagination());
        Tags(ApiTag.Get);
        AllowAnonymous();
        Roles(UserRoles.GetRolesForView());
        Policies(UserPolicies.GetPoliciesForView());
        Permissions(UserPermissions.GetPermissionsForView());
        Summary(s => s.Summary = SummaryCreator.GetPagination(Api));
    }
}
