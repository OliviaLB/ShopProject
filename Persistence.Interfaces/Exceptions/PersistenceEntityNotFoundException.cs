namespace Persistence.Interfaces.Exceptions;

public class PersistenceEntityNotFoundException : Exception
{
    private const string ProductMessageFormat = "Product with ID '{0}' was not found.";
    private const string DefaultNotFoundFormat = "Entity with ID '{0}' was not found.";

    public EntityConstants Entity { get; }
    public Guid EntityId { get; }

    public PersistenceEntityNotFoundException(EntityConstants entity, Guid id) : base(FormatMessage(entity, id))
    {
        Entity = entity;
        EntityId = id;
    }

    private static string FormatMessage(EntityConstants entity, Guid id)
    {
        var format = entity switch
        {
            EntityConstants.Product => ProductMessageFormat,
            _ => DefaultNotFoundFormat
        };

        return string.Format(format, id);
    }
}

