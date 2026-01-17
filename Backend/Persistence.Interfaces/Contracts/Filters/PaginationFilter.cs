namespace Persistence.Interfaces.Contracts.Filters;

public record PaginationFilter
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortField { get; init; }
    public SortDirection SortDirection { get; init; } = SortDirection.Desc;
}
