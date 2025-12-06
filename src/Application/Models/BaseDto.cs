namespace Finisher.Application.Models;

public abstract record BaseNotIdDto;

public abstract record BaseDto : BaseDto<int>;

public abstract record BaseDto<T>
{
    public T Id { get; init; } = default!;
}
