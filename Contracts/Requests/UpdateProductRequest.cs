using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests;

public class UpdateProductRequest
{
    [Required]
    public Guid Id { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [Range(0, long.MaxValue)]
    public long? Price { get; set; }

    public string? PictureUri { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    [Range(0, int.MaxValue)]
    public int? QuantityInStock { get; set; }
}