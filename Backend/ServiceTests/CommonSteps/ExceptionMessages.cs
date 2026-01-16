namespace ServiceTests.CommonSteps;

public static class ExceptionMessages
{
    public static string ProductNotFound(Guid productId)
    {
        return $"Product with ID '{productId}' was not found.";
    }

    public static string UniqueProduct(string title)
    {
        return $"Product already exists with title '{title}'.";
    }

    public static string NameNotEmpty => "'Name' must not be empty.";
}