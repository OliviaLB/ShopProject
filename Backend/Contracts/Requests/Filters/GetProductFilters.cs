namespace Contracts.Filters;

public record GetProductFilters
{
    public Guid[]? Ids { get; init; }
    public string[]? Brands { get; init; }
    public string[]? Types { get; init; }
    public string? SortField { get; init; }
    public bool? InStockOnly { get; init; }
    public string? SearchTerm { get; init; }
}
