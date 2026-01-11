namespace Contracts.Requests;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required long Price { get; set; }
    public required string PictureUri { get; set; }
    public required string Type { get; set; }
    public required string Brand { get; set; }
    public required int QuantityInStock { get; set; }

}
