using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class ChangePasswordCommandHandler
{
    public static async Task HandleAsync(ChangePasswordCommand command, IIdentityService identityService, IUser user)
    {
        await identityService.ChangePasswordAsync(user.Id, command.CurrentPassword, command.NewPassword);
    }
}
