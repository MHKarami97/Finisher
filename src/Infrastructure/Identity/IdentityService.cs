using System.Globalization;
using System.Security;
using System.Security.Claims;
using Finisher.Application.Business.Identity.Queries;
using Finisher.Application.Interfaces.Security;
using Finisher.Application.Interfaces.User;
using Microsoft.AspNetCore.Identity;

namespace Finisher.Infrastructure.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IPhoneSignInCodeService phoneSignInCodeService) : IIdentityService
{
    public async Task<bool> CanSignInAsync(int userId)
    {
        var user = await FindUserAsync(userId);
        return user != null && await signInManager.CanSignInAsync(user);
    }

    public async Task RefreshSignInAsync(int userId)
    {
        var user = await FindUserAsync(userId);
        if (user != null)
        {
            await signInManager.RefreshSignInAsync(user);
        }
    }

    public Task SignOutAsync() =>
        signInManager.SignOutAsync();

    public async Task ResetLockout(int userId)
    {
        var user = await FindUserAsync(userId);
        if (user != null)
        {
            await userManager.SetLockoutEndDateAsync(user, null);
            await userManager.ResetAccessFailedCountAsync(user);
        }
    }

    public int GetUserId(ClaimsPrincipal principal) =>
        int.TryParse(userManager.GetUserId(principal), NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)
            ? id
            : throw new SecurityException(Messages.UserNotFound);

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var user = await FindUserAsync(userId);

        return user == null
            ? throw new Exception(Messages.UserNotFound)
            : new UserDto { Id = user.Id, Phone = user.PhoneNumber, FullName = user.FullName };
    }

    public async Task<UserDto> GetUserByPhoneAsync(string phone)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone && u.PhoneNumberConfirmed);

        return user == null
            ? throw new Exception(Messages.UserNotFound)
            : new UserDto { Id = user.Id, Phone = user.PhoneNumber, FullName = user.FullName };
    }

    public async Task UpdateAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            throw new Exception(Messages.UserNotFound);
        }

        await userManager.UpdateAsync(user);
    }

    public async Task<int> SignInAsync(string phone)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone && u.PhoneNumberConfirmed);

        if (user == null)
        {
            throw new UnauthorizedAccessException(Messages.UserNotFound);
        }

        await phoneSignInCodeService.CreateCodeAsync(phone);

        return user.Id;
    }

    public async Task<bool> VerifySignInCodeAsync(string phone, string code)
    {
        _ = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone && u.PhoneNumberConfirmed) ?? throw new UnauthorizedAccessException(Messages.UserNotFound);
        var isValid = await phoneSignInCodeService.ValidateCodeAsync(phone, code);

        return !isValid ? throw new UnauthorizedAccessException(Messages.InvalidVerificationCode) : isValid;
    }

    public async Task<int> SignUpAsync(string phone)
    {
        var user = new ApplicationUser { PhoneNumber = phone, UserName = phone };
        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(MainConsts.JoinSeparator, result.Errors.Select(e => e.Description)));
        }

        return user.Id;
    }

    public async Task ChangePasswordAsync(int userId, string? currentPassword, string newPassword)
    {
        var user = await FindUserAsync(userId);
        if (user == null)
        {
            throw new Exception(Messages.UserNotFound);
        }

        var hasPassword = await userManager.HasPasswordAsync(user);

        IdentityResult result;

        if (hasPassword)
        {
            if (currentPassword.IsNullOrEmpty())
            {
                throw new ArgumentException(Messages.CurrentPasswordRequired, nameof(currentPassword));
            }

            result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        else
        {
            result = await userManager.AddPasswordAsync(user, newPassword);
        }

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(MainConsts.JoinSeparator, result.Errors.Select(e => e.Description)));
        }
    }

    public async Task ConfirmEmailAsync(int userId, string token)
    {
        var user = await FindUserAsync(userId);
        if (user == null)
        {
            throw new Exception(Messages.UserNotFound);
        }

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(Messages.EmailConfirmationFailed);
        }
    }

    public async Task SendForgotPasswordAsync(int userId)
    {
        var user = await FindUserAsync(userId);
        if (user == null)
        {
            throw new Exception(Messages.UserNotFound);
        }

        _ = await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IReadOnlyList<string>> GetRolesAsync(int userId)
    {
        var user = await FindUserAsync(userId) ?? throw new UnauthorizedAccessException(Messages.UserNotFound);
        var roles = await userManager.GetRolesAsync(user);

        return roles.AsReadOnly();
    }

    private async Task<ApplicationUser?> FindUserAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToInvariantString());
        return user;
    }
}
