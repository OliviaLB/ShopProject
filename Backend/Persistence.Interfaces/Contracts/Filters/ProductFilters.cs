namespace Persistence.Interfaces.Contracts.Filters;

public record ProductFilters
{
    public Guid[]? Ids { get; init; }
    public string[]? Brands { get; init; }
    public string[]? Types { get; init; }
    public bool? InStockOnly { get; init; }
    public string? SearchTerm { get; init; }
}
