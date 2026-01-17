using FiltersContract = Contracts.Filters;
using PersistanceContract = Persistence.Interfaces.Contracts;
using PersistanceFilters = Persistence.Interfaces.Contracts.Filters;
using RequestContract = Contracts.Requests;

namespace Domain.Mapper;

public partial class Mapper
{
    public PersistanceFilters.PaginationFilter MapToPersistence(FiltersContract.PaginationFilter filter)
    {
        return new PersistanceFilters.PaginationFilter
        {
            SortField = filter.SortField,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
            SortDirection = MapToPersistence(filter.SortDirection),
        };
    }

    public PersistanceFilters.SortDirection MapToPersistence(FiltersContract.SortDirection sort)
    {
        return sort switch
        {
            FiltersContract.SortDirection.Asc => PersistanceFilters.SortDirection.Asc,
            FiltersContract.SortDirection.Desc => PersistanceFilters.SortDirection.Desc,
            _ => throw new ArgumentOutOfRangeException(
                    nameof(sort), sort, "Unsupported sort direction")
        };
    }

    public PersistanceFilters.ProductFilters MapToPersistence(FiltersContract.GetProductFilters filters)
    {
        return new PersistanceFilters.ProductFilters
        {
            Ids = filters.Ids,
            Brands = filters.Brands,
            Types = filters.Types,
            InStockOnly = filters.InStockOnly,
            SearchTerm = filters.SearchTerm,
        };
    }

    public PersistanceContract.Product MapToPersistence(RequestContract.CreateProductRequest request)
    {
        return new PersistanceContract.Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            PictureUri = request.PictureUri,
            Type = request.Type,
            Brand = request.Brand,
            QuantityInStock = request.QuantityInStock,
            DateAdded = DateTime.Now
        };
    }

    public PersistanceContract.Product MapToPersistence(RequestContract.UpdateProductRequest request, PersistanceContract.Product product, string? normalisedName)
    {
        return new PersistanceContract.Product
        {
            Id = request.Id,
            Name = normalisedName ?? product.Name,
            Description = request.Description ?? product.Description,
            Price = request.Price ?? product.Price,
            PictureUri = request.PictureUri ?? product.PictureUri,
            Type = request.Type ?? product.Type,
            Brand = request.Brand ?? product.Brand,
            QuantityInStock = request.QuantityInStock ?? product.QuantityInStock,
            ChangeTimestamp = DateTime.UtcNow
        };
    }
}
