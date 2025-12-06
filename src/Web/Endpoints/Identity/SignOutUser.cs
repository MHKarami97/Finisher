using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

internal class SignOutUser(IMessageBus bus) : Endpoint<SignOutUserCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(SignOutUser));
        Roles(UserRoles.GetRolesForView());
        Policies(UserPolicies.CanIdentity);
        Permissions(UserPermissions.CanIdentity);
        Summary(s => s.Summary = nameof(SignOutUser));
    }

    public override async Task HandleAsync(SignOutUserCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
