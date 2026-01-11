namespace Contracts.Response;

public class ProductResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required long Price { get; init; }
    public required string PictureUri { get; init; }
    public required string Type { get; init; }
    public required string Brand { get; init; }
    public required int QuantityInStock { get; init; }
    public DateTime ChangeTimestamp { get; set; }
    public DateTime DateAdded { get; set; }
}
