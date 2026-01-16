using Contracts.Requests;
using Contracts.Response;
using Domain.Exceptions;
using Domain.Mapper;
using Persistence.Interfaces.Writers;

namespace Domain.Services.Products;

public class ProductCreationService : IProductCreationService
{
    private readonly IProductUniqueCheckService _productUniqueCheckService;
    private readonly IProductWriter _productWriter;
    private readonly IMapper _mapper;

    public ProductCreationService(IProductUniqueCheckService productUniqueCheckService, IProductWriter productWriter, IMapper mapper)
    {
        _productUniqueCheckService = productUniqueCheckService;
        _productWriter = productWriter;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var normalisedName = request.Name.Trim();
        request.Name = normalisedName;

        var isUnique = await _productUniqueCheckService.IsUnique(request.Name, null, cancellationToken);
        
        if (!isUnique)
        {
            throw new UniqueProductException(request.Name);
        }

        var product = _mapper.MapToPersistence(request);

        await _productWriter.Upsert(product, cancellationToken);

        return _mapper.MapToResponse(product);
    }
}
