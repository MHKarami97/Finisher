using System.Globalization;
using System.Security;
using System.Security.Claims;
using Finisher.Application.Interfaces.User;
using Finisher.Shared.Consts.Identity;
using Finisher.Shared.Resources;

namespace Finisher.Web.Services;

internal class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public int Id
    {
        get
        {
            var idValue = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimType.Id);
            return int.TryParse(idValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)
                ? id
                : throw new SecurityException(Messages.UserNotFound);
        }
    }

    public int IdIfLoggedIn
    {
        get
        {
            var idValue = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimType.Id);
            return int.TryParse(idValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)
                ? id
                : 0;
        }
    }
}
