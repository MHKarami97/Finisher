using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class SignUpUserCommandHandler
{
    public static async Task HandleAsync(SignUpUserCommand command, IIdentityService identityService)
    {
        _ = await identityService.SignUpAsync(command.Phone);
    }
}
