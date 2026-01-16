namespace Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid id)
    {
        Id = id;
    }

    private Guid Id { get; }

    public override string Message => $"Product with ID '{Id}' was not found.";
}
