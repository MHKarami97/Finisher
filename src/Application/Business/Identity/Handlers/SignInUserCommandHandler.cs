using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class SignInUserCommandHandler
{
    public static async Task HandleAsync(SignInUserCommand command, IIdentityService identityService)
    {
        _ = await identityService.SignInAsync(command.Phone);
    }
}
