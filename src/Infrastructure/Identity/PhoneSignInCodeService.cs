using Finisher.Application.Interfaces.Security;
using Finisher.Shared.Consts.Identity;

namespace Finisher.Infrastructure.Identity;

public sealed class PhoneSignInCodeService(TimeProvider dateTime) : IPhoneSignInCodeService
{
    private static readonly Dictionary<string, (string Code, DateTimeOffset Expiry)> CodeStore = [];
    private static readonly TimeSpan CodeExpiry = TimeSpan.FromMinutes(5);
    private static readonly Random Random = new();

    public Task<string> CreateCodeAsync(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException(Messages.RequiredPhone, nameof(phone));
        }

        var code = GenerateCode();
        var expiry = dateTime.GetUtcNow().Add(CodeExpiry);

        CodeStore[phone] = (code, expiry);

        return Task.FromResult(code);
    }

    public Task<bool> ValidateCodeAsync(string phone, string code)
    {
        if (!CodeStore.TryGetValue(phone, out var entry))
        {
            return Task.FromResult(false);
        }

        if (entry.Expiry < dateTime.GetUtcNow())
        {
            CodeStore.Remove(phone);
            return Task.FromResult(false);
        }

        var isValid = string.Equals(entry.Code, code, StringComparison.Ordinal);

        if (isValid)
        {
            CodeStore.Remove(phone);
        }

        return Task.FromResult(isValid);
    }

    private static string GenerateCode()
    {
        return Random.Next(IdentityConsts.MinVerifyCodeValue, IdentityConsts.MaxVerifyCodeValue).ToInvariantString();
    }
}
