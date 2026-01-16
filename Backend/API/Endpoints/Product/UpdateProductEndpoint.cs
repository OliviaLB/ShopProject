using API.Exceptions;
using Contracts.Requests;
using Contracts.Response;
using Domain.Mapper;
using Domain.Services.Products;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Product;

public static class UpdateProductEndpoint
{
    public static IEndpointRouteBuilder MapUpdateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPatch("{id:guid}", UpdateProduct)
           .WithSummary("Update a product")
           .WithDescription("Partially updates an existing product using the supplied fields and returns the updated product.");

        return app;
    }

    private static async Task<Results<Ok<ProductResponse>, NotFound, BadRequest>> UpdateProduct(
        [FromBody] UpdateProductRequest updateProductRequest,
        [FromServices] IValidator<UpdateProductRequest> updateProductRequestValidator,
        [FromServices] IProductUpdateService productUpdateService,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var validationResult = await updateProductRequestValidator.ValidateAsync(updateProductRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ProductValidationException(validationResult);
        }

        var product = await productUpdateService.Update(updateProductRequest, cancellationToken);

        return TypedResults.Ok(product);
    }
}
