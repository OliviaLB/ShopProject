using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Contracts.Filters;
using Persistence.Interfaces.Readers;
using Persistence.SqlServer.Extensions;

namespace Persistence.SqlServer.Readers;

public class ProductReader : IProductReader
{
    private readonly ShopDbContext _dbContext;

    public ProductReader(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetSingle(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Products.SingleOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PagedList<Product>> GetAll(
        ProductFilters filters, 
        PaginationFilter pagination,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Products
        .AsNoTracking()
        .WhereIf(filters.Ids?.Length > 0, p => filters.Ids!.Contains(p.Id))
        .WhereIf(filters.Brands?.Length > 0, p => filters.Brands!.Contains(p.Brand))
        .WhereIf(filters.Types?.Length > 0, p => filters.Types!.Contains(p.Type))
        .WhereIf(filters.InStockOnly == true, p => p.QuantityInStock > 0)
        .WhereIf(!string.IsNullOrWhiteSpace(filters.SearchTerm),
            p => p.Name.Contains(filters.SearchTerm!) ||
                 p.Description.Contains(filters.SearchTerm!));

        var desc = pagination.SortDirection == SortDirection.Desc;

        query = filters.SortField switch
        {
            nameof(Product.Name) => desc
                ? query.OrderByDescending(p => p.Name).ThenByDescending(p => p.Id)
                : query.OrderBy(p => p.Name).ThenBy(p => p.Id),

            nameof(Product.Price) => desc
                ? query.OrderByDescending(p => p.Price).ThenByDescending(p => p.Id)
                : query.OrderBy(p => p.Price).ThenBy(p => p.Id),

            nameof(Product.QuantityInStock) => desc
                ? query.OrderByDescending(p => p.QuantityInStock).ThenByDescending(p => p.Id)
                : query.OrderBy(p => p.QuantityInStock).ThenBy(p => p.Id),

            nameof(Product.DateAdded) => desc
                ? query.OrderByDescending(p => p.DateAdded).ThenByDescending(p => p.Id)
                : query.OrderBy(p => p.DateAdded).ThenBy(p => p.Id),

            _ => desc
                ? query.OrderByDescending(p => p.Name).ThenByDescending(p => p.Id)
                : query.OrderBy(p => p.Name).ThenBy(p => p.Id)
        };

        var totalItems = await query.CountAsync(cancellationToken);

        if (pagination.ReturnAll == true)
        {
            var allItems = await query.ToListAsync(cancellationToken);
            return new PagedList<Product>(
                allItems,
                pageNumber: 1,
                pageSize: allItems.Count,
                totalItems: allItems.Count);
        }

        var pageNumber = pagination.PageNumber < 1 ? 1 : pagination.PageNumber;
        var pageSize = pagination.PageSize < 1 ? 10 : pagination.PageSize;
        var skip = (pageNumber - 1) * pageSize;

        var items = await query
       .Skip(skip)
       .Take(pageSize)
       .ToListAsync(cancellationToken);

        return new PagedList<Product>(items, pageNumber, pageSize, totalItems);
    }
}
