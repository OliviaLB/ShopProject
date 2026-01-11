using Persistence.Interfaces.Contracts;

namespace Persistence.Interfaces.Writers;

public interface IProductWriter
{
    Task Upsert(Product product, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}
