namespace Finisher.Application.Business.Identity.Queries;

public sealed record UserDto : BaseDto
{
    public required string? Phone { get; init; }
    public required string? FullName { get; init; }
}
