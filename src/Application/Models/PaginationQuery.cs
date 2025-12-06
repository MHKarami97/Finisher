namespace Finisher.Application.Models;

public class PaginationQueryValidator : AbstractValidator<PaginationQuery>
{
    public PaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .ExclusiveBetween(PaginationConsts.PaginationMinPageNumber, PaginationConsts.PaginationMaxPageNumber)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(PaginationConsts.PaginationMinPageNumber, PaginationConsts.PaginationMaxPageNumber));

        RuleFor(x => x.PageSize)
            .ExclusiveBetween(PaginationConsts.PaginationMinPageSize, PaginationConsts.PaginationMaxPageSize)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(PaginationConsts.PaginationMinPageSize, PaginationConsts.PaginationMaxPageSize));
    }
}
