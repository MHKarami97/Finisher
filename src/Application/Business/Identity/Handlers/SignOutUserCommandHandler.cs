using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class SignOutUserCommandHandler
{
    public static async Task HandleAsync(SignOutUserCommand command, IIdentityService identityService)
    {
        await identityService.SignOutAsync();
    }
}
