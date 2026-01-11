using Contracts.Response;
using Persistence.Interfaces.Contracts;

namespace Domain.Mapper;

public partial class Mapper
{
    public ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUri = product.PictureUri,
            Type = product.Type,
            Brand = product.Brand,
            QuantityInStock = product.QuantityInStock,
            ChangeTimestamp = product.ChangeTimestamp,
            DateAdded = product.DateAdded,
        };
    }

    public List<ProductResponse> MapToResponse(List<Product> products)
    {
        return products.Select(MapToResponse).ToList();
    }

    public Contracts.Pagination MapToResponse(Pagination pagination)
    {
        return new Contracts.Pagination
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalItems = pagination.TotalItems,
            TotalPages = pagination.TotalPages
        };
    }

    public Contracts.PagedList<ProductResponse> MapToResponse(PagedList<Product> pagedProducts)
    {
        return new Contracts.PagedList<ProductResponse>
        {
            Items = MapToResponse(pagedProducts.Items),
            Pagination = MapToResponse(pagedProducts.Pagination)
        };
    }
}
