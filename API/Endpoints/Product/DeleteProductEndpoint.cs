using Domain.Services.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Product;

public static class DeleteProductEndpoint
{
    public static IEndpointRouteBuilder MapDeleteProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", DeleteProduct)
           .WithSummary("Delete a product")
           .WithDescription("Deletes the product that matches the supplied product ID.");
        return app;
    }

    private static async Task<Results<NoContent, NotFound>> DeleteProduct(
        [FromRoute] Guid id,
        [FromServices] IProductDeletionService productDeletionService,
        CancellationToken cancellationToken)
    {
        await productDeletionService.Delete(id, cancellationToken);

        return TypedResults.NoContent();
    }
}
