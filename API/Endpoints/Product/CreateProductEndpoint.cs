using API.Exceptions;
using Contracts.Requests;
using Contracts.Response;
using Domain.Services.Products;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Product;

public static class CreateProductEndpoint
{
    public static IEndpointRouteBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(string.Empty, CreateProduct)
           .WithSummary("Create a product")
           .WithDescription("Creates a new product and returns the created product resource.");
        return app;
    }

    private static async Task<Results<Created<ProductResponse>, BadRequest>> CreateProduct(
        [FromBody] CreateProductRequest createProductRequest,
        [FromServices] IValidator<CreateProductRequest> createProductRequestValidator,
        [FromServices] IProductCreationService movieCreationService,
        CancellationToken cancellationToken)
    {
        var validationResult =
            await createProductRequestValidator.ValidateAsync(createProductRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ProductValidationException(validationResult);
        }

        var product = await movieCreationService.Create(createProductRequest, cancellationToken);

        return TypedResults.Created($"products/{product.Id}", product);
    }
}
