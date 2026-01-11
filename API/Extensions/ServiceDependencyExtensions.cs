using API.Validation;
using Contracts.Requests;
using Domain.Mapper;
using Domain.Services.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Readers;
using Persistence.Interfaces.Writers;
using Persistence.SqlServer;
using Persistence.SqlServer.Readers;
using Persistence.SqlServer.Writers;

namespace API.Extensions;

public static class ServiceDependencyExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>();

        services.AddScoped<IProductCreationService, ProductCreationService>();
        services.AddScoped<IProductDeletionService, ProductDeletionService>();
        services.AddScoped<IProductRetrievalService, ProductRetrievalService>();
        services.AddScoped<IProductUpdateService, ProductUpdateService>();

        services.AddScoped<IProductUniqueCheckService, ProductUniqueCheckService>();

        services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
        services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();

        return services;
    }

    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShopDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly("Persistence.SqlServer")
           ));

        services.AddScoped<IProductReader, ProductReader>();
        services.AddScoped<IProductWriter, ProductWriter>();

        return services;
    }
}
