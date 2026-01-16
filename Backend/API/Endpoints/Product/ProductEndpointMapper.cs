using Api.Endpoints;
using API.Endpoints.Product;

namespace API.Endpoints.Products;

public static class ProductEndpointMapper
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointGroup = app.MapGroup("products")
            .WithTags(OpenApiTags.Products);

        endpointGroup
            .MapCreateProductEndpoint()
            .MapDeleteProductEndpoint()
            .MapGetProductsEndpoint()
            .MapGetProductById()
            .MapUpdateProductEndpoint();

        return app;
    }
}
