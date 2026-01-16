using Contracts.Response;
using Domain.Services.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Product;

public static class GetProductByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetProductById(this IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", GetProductById)
           .WithSummary("Get product by ID")
           .WithDescription("Retrieves the product that matches the supplied product ID.");
        return app;
    }

    private static async Task<Results<Ok<ProductResponse>, NotFound>> GetProductById(
        [FromRoute] Guid id,
        [FromServices] IProductRetrievalService productRetrievalService,
        CancellationToken cancellationToken)
    {
        var product = await productRetrievalService.GetById(id, cancellationToken);

        return TypedResults.Ok(product);
    }
}
