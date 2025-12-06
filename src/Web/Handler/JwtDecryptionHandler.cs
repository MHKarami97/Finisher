using System.Text.Encodings.Web;
using Finisher.Application.Interfaces.Security;
using Finisher.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Finisher.Web.Handler;

internal sealed class JwtDecryptionHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IJwtTokenService jwtService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.EnsureStartsWith("Bearer "))
        {
            return AuthenticateResult.NoResult();
        }

        var token = authHeader["Bearer ".Length..];

        try
        {
            var decoded = await jwtService.DecodeTokenAsync(token);
            var principal = jwtService.ToPrincipal(decoded);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (SecurityTokenException ex)
        {
            return AuthenticateResult.Fail($"Token error: {ex.Message}");
        }
    }
}

internal static class CustomJwt
{
    public const string AuthSchemaName = nameof(AuthSchemaName);
}
