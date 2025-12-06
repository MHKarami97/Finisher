namespace Finisher.Application.Dto;

public sealed record DecodedJwtDto(
    string UserId,
    string? Phone,
    string? FullName,
    IReadOnlyList<string> Roles,
    IReadOnlyList<string> Policies,
    IReadOnlyList<string> Permissions
);
