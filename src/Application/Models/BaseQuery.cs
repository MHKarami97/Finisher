namespace Finisher.Application.Models;

public abstract record BaseNotIdQuery;

public abstract record BaseQuery(int Id) : BaseQuery<int>(Id);

public abstract record BaseQuery<T>(T Id);

public class BaseQueryValidator : AbstractValidator<BaseQuery>
{
    public BaseQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(MainConsts.Zero)
            .WithMessage(_ => Messages.GreaterThanZero);
    }
}
