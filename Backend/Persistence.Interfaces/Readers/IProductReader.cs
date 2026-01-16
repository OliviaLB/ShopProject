using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Contracts.Filters;

namespace Persistence.Interfaces.Readers;

public interface IProductReader
{
    Task<Product?> GetSingle(string name, CancellationToken cancellationToken);

    Task<Product?> GetById(Guid id, CancellationToken cancellationToken);

    Task<PagedList<Product>> GetAll(
        ProductFilters filters,
        PaginationFilter pagination,
        CancellationToken cancellationToken);
}
