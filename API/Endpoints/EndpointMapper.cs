using API.Endpoints.Products;

namespace API.Endpoints;

public static class EndpointMapper
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductEndpoints();
        return app;
    }
}