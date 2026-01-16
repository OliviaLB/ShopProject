using FiltersContract = Contracts.Filters;
using PersistanceContract = Persistence.Interfaces.Contracts;
using PersistanceFilters = Persistence.Interfaces.Contracts.Filters;
using RequestContract = Contracts.Requests;

namespace Domain.Mapper;

public interface IProductPersistenceMapper
{
    PersistanceFilters.PaginationFilter MapToPersistence(FiltersContract.PaginationFilter filter);

    PersistanceFilters.SortDirection MapToPersistence(FiltersContract.SortDirection sort);

    PersistanceFilters.ProductFilters MapToPersistence(FiltersContract.GetProductFilters filters);

    PersistanceContract.Product MapToPersistence(RequestContract.CreateProductRequest request);

    PersistanceContract.Product MapToPersistence(RequestContract.UpdateProductRequest request, PersistanceContract.Product product, string? normalisedName);

}
