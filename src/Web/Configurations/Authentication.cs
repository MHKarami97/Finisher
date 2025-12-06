using System.Text;
using Finisher.Shared.Consts.Identity;
using Finisher.Web.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Finisher.Web.Configurations;

internal static class Authentication
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var setting = configuration
            .GetSection("Jwt")
            .Get<JwtOptions>()!;

        services.AddAuthentication(CustomJwt.AuthSchemaName)
            .AddScheme<AuthenticationSchemeOptions, JwtDecryptionHandler>(CustomJwt.AuthSchemaName,
                o => o.ClaimsIssuer = setting.Issuer)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = setting.Issuer,
                ValidAudience = setting.Audience,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimType.Role,
                NameClaimType = ClaimType.Name,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key)),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key)),
            });
    }

    public static void ConfigureAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
    }
}

internal sealed class JwtOptions
{
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int ExpiresInMinutes { get; init; }
}
