namespace Domain.Exceptions;

public class UniqueProductException : Exception
{
    public UniqueProductException(string name)
    {
        Name = name;
    }

    private string Name { get; }

    public override string Message => $"Product already exists with name '{Name}'.";
}
