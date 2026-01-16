namespace Domain.Services.Products;

public interface IProductDeletionService
{
    Task Delete(Guid id, CancellationToken cancellationToken);
}
