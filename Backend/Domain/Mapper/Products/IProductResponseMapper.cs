using Contracts.Response;
using Persistence.Interfaces.Contracts;

namespace Domain.Mapper;

public interface IProductResponseMapper
{
    ProductResponse MapToResponse(Product product);

    List<ProductResponse> MapToResponse(List<Product> products);

    Contracts.Pagination MapToResponse(Pagination pagination);

    Contracts.PagedList<ProductResponse> MapToResponse(PagedList<Product> pagedProducts);
}
