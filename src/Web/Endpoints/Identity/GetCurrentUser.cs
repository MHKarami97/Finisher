using Finisher.Application.Business.Identity.Queries;

namespace Finisher.Web.Endpoints.Identity;

public class GetCurrentUser(IMessageBus bus) : Endpoint<GetCurrentUserQuery, UserDto>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(GetCurrentUser));
        Roles(UserRoles.GetRolesForView());
        Policies(UserPolicies.CanIdentity);
        Permissions(UserPermissions.CanIdentity);
        Summary(s => s.Summary = nameof(GetCurrentUser));
    }

    public override async Task HandleAsync(GetCurrentUserQuery req, CancellationToken ct)
    {
        var user = await bus.InvokeAsync<UserDto>(req, ct);
        await Send.OkAsync(user, ct);
    }
}
