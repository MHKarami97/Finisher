using System.ComponentModel;

namespace Finisher.Domain.Models;

public record PaginationQuery
{
    [DefaultValue(PaginationConsts.PaginationMinPageNumber)]
    public int PageNumber { get; init; } = PaginationConsts.PaginationMinPageNumber;

    [DefaultValue(PaginationConsts.PaginationMinPageSize)]
    public int PageSize { get; init; } = PaginationConsts.PaginationMinPageSize;
}
