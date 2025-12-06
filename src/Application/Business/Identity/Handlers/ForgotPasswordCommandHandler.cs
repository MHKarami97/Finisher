using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class ForgotPasswordCommandHandler
{
    public static async Task HandleAsync(ForgotPasswordCommand command, IIdentityService identityService, IUser user)
    {
        await identityService.SendForgotPasswordAsync(user.Id);
    }
}
