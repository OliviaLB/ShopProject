

using Domain.Exceptions;
using Persistence.Interfaces.Exceptions;
using Persistence.Interfaces.Writers;

namespace Domain.Services.Products;

public class ProductDeletionService : IProductDeletionService
{
    private readonly IProductWriter _productWriter;

    public ProductDeletionService(IProductWriter ProductWriter)
    {
        _productWriter = ProductWriter;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _productWriter.Delete(id, cancellationToken);
        }
        catch (PersistenceEntityNotFoundException)
        {
            throw new ProductNotFoundException(id);
        }
    }
}