using Persistence.Interfaces.Readers;

namespace Domain.Services.Products;

public class ProductUniqueCheckService : IProductUniqueCheckService
{
    private readonly IProductReader _productReader;

    public ProductUniqueCheckService(IProductReader productReader)
    {
        _productReader = productReader;
    }

    public async Task<bool> IsUnique(string name, Guid? id, CancellationToken cancellationToken)
    {
        var existing = await _productReader.GetSingle(name, cancellationToken);
        return existing is null || (id.HasValue && existing.Id == id.Value);
    }
}
