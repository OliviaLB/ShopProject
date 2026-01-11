namespace Persistence.Interfaces.Contracts.Filters;

public class PaginationFilter
{
    public bool ReturnAll { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public SortDirection SortDirection { get; set; } = SortDirection.Desc;
}
