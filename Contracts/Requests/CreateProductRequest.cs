using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests;

public class CreateProductRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = default!;

    [Range(0, long.MaxValue)]
    public long Price { get; set; }

    [Required]
    public string PictureUri { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string Type { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = default!;

    [Range(0, int.MaxValue)]
    public int QuantityInStock { get; set; }

}
