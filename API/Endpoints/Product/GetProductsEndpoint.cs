using Contracts;
using Contracts.Filters;
using Contracts.Response;
using Domain.Services.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Product;

public static class GetProductsEndpoint
{
    public static IEndpointRouteBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, GetProducts)
           .WithSummary("Get products")
           .WithDescription("Retrieves a paginated list of products, optionally filtered by the supplied query parameters.");

        return app;
    }

    private static async Task<Ok<PagedList<ProductResponse>>> GetProducts(
         [AsParameters] GetProductFilters filters,
         [AsParameters] PaginationFilter pagination,
         [FromServices] IProductRetrievalService productRetrievalService,
         CancellationToken cancellationToken)
    {
        var products = await productRetrievalService.GetAll(filters, pagination, cancellationToken);

        return TypedResults.Ok(products);
    }
}
