using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class ConfirmEmailCommandHandler
{
    public static async Task HandleAsync(ConfirmEmailCommand command, IIdentityService identityService, IUser user)
    {
        await identityService.ConfirmEmailAsync(user.Id, command.Token);
    }
}
