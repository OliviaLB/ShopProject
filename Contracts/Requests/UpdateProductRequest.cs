namespace Contracts.Requests;

public class UpdateProductRequest
{
    public required Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? Price { get; set; }
    public string? PictureUri { get; set; }
    public string? Type { get; set; }
    public string? Brand { get; set; }
    public int? QuantityInStock { get; set; }
    public DateTime ChangeTimestamp { get; set; }
}
