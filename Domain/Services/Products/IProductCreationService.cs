using Contracts.Requests;
using Contracts.Response;

namespace Domain.Services.Products;

public interface IProductCreationService
{
    Task<ProductResponse> Create(CreateProductRequest request, CancellationToken cancellationToken);
}
