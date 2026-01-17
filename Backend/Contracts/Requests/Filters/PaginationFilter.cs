namespace Contracts.Filters;

public class PaginationFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortField { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Desc;
}
