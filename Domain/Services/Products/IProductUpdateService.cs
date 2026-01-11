using Contracts.Requests;
using Contracts.Response;

namespace Domain.Services.Products;

public interface IProductUpdateService
{
    Task<ProductResponse> Update(UpdateProductRequest request, CancellationToken cancellationToken);
}
