using Finisher.Application.Business.Identity.Commands;
using Finisher.Application.Interfaces.Security;
using Finisher.Application.Interfaces.User;
using Microsoft.Extensions.Hosting;

namespace Finisher.Application.Business.Identity.Handlers;

public static class VerifySignInCodeUserCommandHandler
{
    public static async Task<string> HandleAsync(VerifySignInCodeUserCommand command, IIdentityService identityService, IHostEnvironment environment, IJwtTokenService jwtTokenService)
    {
        if (environment.IsDevelopment() && command.Phone.EqualsTo(AdminUserConsts.AdminPhoneNumber) && command.Code.EqualsTo(MainConsts.DevelopmentAdminVerifyCode))
        {
            return await OnDevelopmentAsync(command, identityService, jwtTokenService);
        }

        await identityService.VerifySignInCodeAsync(command.Phone, command.Code);

        var user = await identityService.GetUserByPhoneAsync(command.Phone);

        return await jwtTokenService.GenerateTokenAsync(user.Id, user.Phone, user.FullName);
    }

    private static async Task<string> OnDevelopmentAsync(VerifySignInCodeUserCommand command, IIdentityService identityService, IJwtTokenService jwtTokenService)
    {
        var user = await identityService.GetUserByPhoneAsync(command.Phone);

        return await jwtTokenService.GenerateTokenAsync(user.Id, user.Phone, user.FullName);
    }
}
