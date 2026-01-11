using Contracts.Requests;
using FluentValidation;

namespace API.Validation;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(200)
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => x.Description is not null);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Price.HasValue);

        RuleFor(x => x.Type)
            .MaximumLength(100)
            .When(x => x.Type is not null);

        RuleFor(x => x.Brand)
            .MaximumLength(100)
            .When(x => x.Brand is not null);

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0)
            .When(x => x.QuantityInStock.HasValue);

        RuleFor(x => x.ChangeTimestamp)
            .NotEmpty();
    }
}