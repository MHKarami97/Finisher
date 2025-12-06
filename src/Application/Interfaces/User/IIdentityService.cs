using System.Security.Claims;
using Finisher.Application.Business.Identity.Queries;

namespace Finisher.Application.Interfaces.User;

public interface IIdentityService : IIdentityService<int>;

public interface IIdentityService<T>
{
    Task<bool> CanSignInAsync(T userId);
    Task RefreshSignInAsync(T userId);
    Task SignOutAsync();
    Task ResetLockout(T userId);
    T? GetUserId(ClaimsPrincipal principal);
    Task<UserDto> GetUserByIdAsync(T userId);
    Task<UserDto> GetUserByPhoneAsync(string phone);
    Task UpdateAsync(string userName);
    Task<int> SignInAsync(string phone);
    Task<bool> VerifySignInCodeAsync(string phone, string code);
    Task<T> SignUpAsync(string phone);
    Task ChangePasswordAsync(T userId, string? currentPassword, string newPassword);
    Task ConfirmEmailAsync(T userId, string token);
    Task SendForgotPasswordAsync(T userId);
    Task<IReadOnlyList<string>> GetRolesAsync(T userId);
}
