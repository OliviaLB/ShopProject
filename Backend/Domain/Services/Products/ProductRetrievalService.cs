
using Contracts;
using Contracts.Filters;
using Contracts.Response;
using Domain.Exceptions;
using Domain.Mapper;
using Persistence.Interfaces.Readers;

namespace Domain.Services.Products;

public class ProductRetrievalService : IProductRetrievalService
{
    private readonly IProductReader _productReader;
    private readonly IMapper _mapper;

    public ProductRetrievalService(IProductReader productReader, IMapper mapper)
    {
        _productReader = productReader;
        _mapper = mapper;
    }
    public async Task<PagedList<ProductResponse>> GetAll(GetProductFilters filters, PaginationFilter pagination, CancellationToken cancellationToken)
    {
        var persistenceFilters = _mapper.MapToPersistence(filters);
        var persistencePagination = _mapper.MapToPersistence(pagination);

        var pagedProducts = await _productReader.GetAll(persistenceFilters, persistencePagination, cancellationToken);
        
        return  _mapper.MapToResponse(pagedProducts);

        throw new NotImplementedException();
    }

    public async Task<ProductResponse> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productReader.GetById(id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        return _mapper.MapToResponse(product);
    }
}
