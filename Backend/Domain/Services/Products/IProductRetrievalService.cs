using Contracts;
using Contracts.Filters;
using Contracts.Response;

namespace Domain.Services.Products;

public interface IProductRetrievalService
{
    Task<ProductResponse> GetById(Guid id, CancellationToken cancellationToken);

    Task<PagedList<ProductResponse>> GetAll(GetProductFilters filters, PaginationFilter pagination, CancellationToken cancellationToken);
}
