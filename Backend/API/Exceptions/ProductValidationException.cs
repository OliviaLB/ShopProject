

using FluentValidation.Results;

namespace API.Exceptions;

public class ProductValidationException : Exception
{
    public ProductValidationException(ValidationResult validationResult) : base(FlattenErrors(validationResult.ToDictionary())) { }

    private static string FlattenErrors(IDictionary<string, string[]> errors)
    {
        return string.Join("\n", errors.Select(x => $"{x.Key} : '{x.Value.FirstOrDefault()}'"));
    }
}
