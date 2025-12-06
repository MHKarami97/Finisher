namespace Finisher.Web.Endpoints;

public abstract class BaseCreateEndPoint<TRequest, TResponse>(IMessageBus bus, string apiName) : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Post(Api);
        Tags(ApiTag.Create);
        Roles(UserRoles.GetRolesForCreate());
        Policies(UserPolicies.GetPoliciesForCreate());
        Permissions(UserPermissions.GetPermissionsForCreate());
        Summary(s => s.Summary = SummaryCreator.Create(Api));
    }
}

public abstract class BaseCreateEndPoint<TRequest>(IMessageBus bus, string apiName) : Endpoint<TRequest>
    where TRequest : notnull
{
    protected IMessageBus Bus { get; } = bus;
    private string Api { get; } = apiName;

    public override void Configure()
    {
        Post(Api);
        Tags(ApiTag.Create);
        Roles(UserRoles.GetRolesForCreate());
        Policies(UserPolicies.GetPoliciesForCreate());
        Permissions(UserPermissions.GetPermissionsForCreate());
        Summary(s => s.Summary = SummaryCreator.Create(Api));
    }
}
