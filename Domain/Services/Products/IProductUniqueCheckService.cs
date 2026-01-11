namespace Domain.Services.Products;

public interface IProductUniqueCheckService
{
    Task<bool> IsUnique(string name, Guid? id, CancellationToken cancellationToken);
}
