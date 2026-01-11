using Contracts.Requests;
using Contracts.Response;
using Domain.Exceptions;
using Domain.Mapper;
using Persistence.Interfaces.Readers;
using Persistence.Interfaces.Writers;

namespace Domain.Services.Products;

public class ProductUpdateService : IProductUpdateService
{
    private readonly IProductReader _productReader;
    private readonly IProductUniqueCheckService _productUniqueCheckService;
    private readonly IProductWriter _productWriter;
    private readonly IMapper _mapper;

    public ProductUpdateService(IProductReader productReader, IProductUniqueCheckService productUniqueCheckService, IProductWriter productWriter, IMapper mapper)
    {
        _productReader = productReader;
        _productUniqueCheckService = productUniqueCheckService;
        _productWriter = productWriter;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Update(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReader.GetById(request.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException(request.Id);

        string? normalisedName = request.Name?.Trim();

        if (normalisedName is not null &&
       !string.Equals(product.Name, normalisedName, StringComparison.Ordinal))
        {
            var isUnique = await _productUniqueCheckService.IsUnique(normalisedName, request.Id, cancellationToken);
            if (!isUnique) throw new UniqueProductException(normalisedName);
        }

        var updatedProduct = _mapper.MapToPersistence(request, product, normalisedName);

        await _productWriter.Upsert(updatedProduct, cancellationToken);
        return _mapper.MapToResponse(updatedProduct);
    }
}
