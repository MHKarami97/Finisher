namespace Finisher.Application.Interfaces.Security;

public interface IPhoneSignInCodeService
{
    Task<string> CreateCodeAsync(string phone);
    Task<bool> ValidateCodeAsync(string phone, string code);
}
