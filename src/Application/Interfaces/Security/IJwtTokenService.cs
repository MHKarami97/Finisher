using System.Security.Claims;
using Finisher.Application.Dto;

namespace Finisher.Application.Interfaces.Security;

public interface IJwtTokenService : IJwtTokenService<int>;

public interface IJwtTokenService<in T>
{
    Task<string> GenerateTokenAsync(T userId, string? phone, string? fullName);
    Task<DecodedJwtDto> DecodeTokenAsync(string token);
    ClaimsPrincipal ToPrincipal(DecodedJwtDto dto);
}
