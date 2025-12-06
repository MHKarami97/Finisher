using Finisher.Application.Business.Identity.Queries;
using Finisher.Application.Interfaces.User;

namespace Finisher.Application.Business.Identity.Handlers;

public static class GetCurrentUserQueryHandler
{
    public static async Task<UserDto> HandleAsync(GetCurrentUserQuery query, IIdentityService identityService, IUser user)
    {
        return await identityService.GetUserByIdAsync(user.Id);
    }
}
