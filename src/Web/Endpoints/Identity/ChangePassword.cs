using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

internal class ChangePassword(IMessageBus bus) : Endpoint<ChangePasswordCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(ChangePassword));
        Roles(UserRoles.GetRolesForView());
        Policies(UserPolicies.CanIdentity);
        Permissions(UserPermissions.CanIdentity);
        Summary(s => s.Summary = nameof(ChangePassword));
    }

    public override async Task HandleAsync(ChangePasswordCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
