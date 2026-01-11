using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerDependencyExtensions
{
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Shop Service Api",
                    Version = "v1",
                    Description = "Shop Service Api",
                    Contact = new OpenApiContact { Name = "Corporation", Email = "tbc@tbc.com" }
                });

            options.UseInlineDefinitionsForEnums();
        });
    }
}
