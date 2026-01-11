using Persistence.Interfaces;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Exceptions;
using Persistence.Interfaces.Readers;
using Persistence.Interfaces.Writers;

namespace Persistence.SqlServer.Writers;

public class ProductWriter : IProductWriter
{

    private readonly IProductReader _ProductReader;
    private readonly ShopDbContext _dbContext;

    public ProductWriter(IProductReader ProductReader, ShopDbContext dbContext)
    {
        _ProductReader = ProductReader;
        _dbContext = dbContext;
    }

    public async Task Upsert(Product product, CancellationToken cancellationToken)
    {
        var dbProduct = await _ProductReader.GetById(product.Id, cancellationToken);

        if (dbProduct == null)
        {
            _dbContext.Products.Add(product);
        }

        else
        {
            if (dbProduct.ChangeTimestamp > product.ChangeTimestamp)
            {
                return;
            }

            _dbContext.Entry(dbProduct).CurrentValues.SetValues(product);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _ProductReader.GetById(id, cancellationToken);

        if (entity == null)
        {
            throw new PersistenceEntityNotFoundException(EntityConstants.Product, id);
        }

        _dbContext.Products.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
