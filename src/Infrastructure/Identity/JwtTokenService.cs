using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Finisher.Application.Dto;
using Finisher.Application.Interfaces.Security;
using Finisher.Application.Interfaces.User;
using Finisher.Domain.Constants;
using Finisher.Shared.Consts.Identity;
using Jose;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Finisher.Infrastructure.Identity;

public sealed class JwtTokenService(IIdentityService identityService, IConfiguration configuration, TimeProvider dateTime) : IJwtTokenService
{
    private const JweAlgorithm Algorithm = JweAlgorithm.DIR;
    private const JweEncryption Encryption = JweEncryption.A128CBC_HS256;

    public async Task<string> GenerateTokenAsync(int userId, string? phone, string? fullName)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var expires = dateTime.GetUtcNow().AddMinutes(double.Parse(configuration["Jwt:ExpiresInMinutes"]!, CultureInfo.InvariantCulture));

        var roles = await identityService.GetRolesAsync(userId);
        var (policies, permissions) = MapClaimsForRoles(roles);

        var claims = new Dictionary<string, object>
        {
            { ClaimType.Id, userId },
            { ClaimType.Name, fullName ?? "" },
            { ClaimType.Phone, phone ?? "" },
            { "exp", expires.ToUnixTimeSeconds() },
            { "iss", issuer },
            { "aud", audience },
            { "nbf", dateTime.GetUtcNow().ToUnixTimeSeconds() },
            { ClaimType.Role, roles.ToArray() },
            { ClaimType.Permission, permissions.ToArray() },
            { ClaimType.Policy, policies.ToArray() }
        };

        return JWT.Encode(claims, key, Algorithm, Encryption);
    }

    public Task<DecodedJwtDto> DecodeTokenAsync(string token)
    {
        try
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            var audience = configuration["Jwt:Audience"]!;
            var now = dateTime.GetUtcNow().ToUnixTimeSeconds();

            var payload = JWT.Decode<Dictionary<string, object>>(token, key, Algorithm, Encryption);

            if (!payload.TryGetValue("exp", out var expObj) || !long.TryParse(expObj.ToString(), out var exp))
            {
                throw new SecurityTokenException(Messages.TokenException);
            }

            if (now >= exp)
            {
                throw new SecurityTokenExpiredException(Messages.ExpiredToken);
            }

            if (payload.TryGetValue("nbf", out var nbfObj) && long.TryParse(nbfObj.ToString(), out var nbf))
            {
                if (now < nbf)
                {
                    throw new SecurityTokenException(Messages.TokenException);
                }
            }

            if (!payload.TryGetValue("aud", out var audObj) || audObj.ToString() != audience)
            {
                throw new SecurityTokenException(Messages.TokenException);
            }

            var userId = payload.TryGetValue(ClaimType.Id, out var idObj) ? idObj.ToString()! : throw new SecurityTokenException(Messages.TokenException);
            var fullName = payload.TryGetValue(ClaimType.Name, out var fullNameObj) ? fullNameObj.ToString() : null;
            var phone = payload.TryGetValue(ClaimType.Phone, out var phoneObj) ? phoneObj.ToString() : null;

            var roles = payload.TryGetValue(ClaimType.Role, out var rolesObj)
                ? ParseToList(rolesObj)
                : [];

            var policies = payload.TryGetValue(ClaimType.Policy, out var policiesObj)
                ? ParseToList(policiesObj)
                : [];

            var permissions = payload.TryGetValue(ClaimType.Permission, out var permissionsObj)
                ? ParseToList(permissionsObj)
                : [];

            return Task.FromResult(new DecodedJwtDto(
                UserId: userId,
                Phone: phone,
                FullName: fullName,
                Roles: roles,
                Policies: policies,
                Permissions: permissions
            ));
        }
        catch (IntegrityException ex)
        {
            throw new SecurityTokenException(Messages.TokenException, ex);
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException(Messages.TokenException, ex);
        }
    }

    public ClaimsPrincipal ToPrincipal(DecodedJwtDto dto)
    {
        var claims = new List<Claim>
        {
            new(ClaimType.Id, dto.UserId),
            new(ClaimType.Name, dto.FullName ?? ""),
            new(ClaimType.Phone, dto.Phone ?? "")
        };

        claims.AddRange(dto.Roles.Select(r => new Claim(ClaimType.Role, r)));
        claims.AddRange(dto.Policies.Select(p => new Claim(ClaimType.Policy, p)));
        claims.AddRange(dto.Permissions.Select(p => new Claim(ClaimType.Permission, p)));

        var identity = new ClaimsIdentity(claims, "EncryptedJwt");

        return new ClaimsPrincipal(identity);
    }

    private static (IEnumerable<string> Policies, IEnumerable<string> Permissions) MapClaimsForRoles(IEnumerable<string> roles)
    {
        var policySet = new HashSet<string>();
        var permissionSet = new HashSet<string>();

        foreach (var role in roles)
        {
            policySet.UnionWith(role switch
            {
                UserRoles.Administrator => UserPolicies.GetPoliciesForAdmin(),
                UserRoles.Moderator => UserPolicies.GetPoliciesForModerator(),
                UserRoles.User => UserPolicies.GetPoliciesForUser(),
                _ => []
            });

            permissionSet.UnionWith(role switch
            {
                UserRoles.Administrator => UserPermissions.GetPermissionsForAdmin(),
                UserRoles.Moderator => UserPermissions.GetPermissionsForModerator(),
                UserRoles.User => UserPermissions.GetPermissionsForUser(),
                _ => []
            });
        }

        return (policySet, permissionSet);
    }

    private static IReadOnlyList<string> ParseToList(object value)
    {
        return value switch
        {
            string str => [str],
            JsonElement { ValueKind: JsonValueKind.Array } arr => arr.EnumerateArray().Select(e => e.GetString()!).ToList(),
            IEnumerable<object> objList => objList.Select(o => o.ToString()!).ToList(),
            _ => throw new InvalidOperationException(Messages.UnhandledException)
        };
    }
}
