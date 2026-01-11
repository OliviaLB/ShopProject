using AutoFixture;
using Contracts.Response;
using Shouldly;
using System.Net.Http.Json;

namespace ServiceTests.Products;

public partial class Get_Product_By_Id_Feature : FeatureFixture
{
    private readonly Guid _organisationId;
    private readonly Guid _requestId;
    private readonly Guid _productId;

    private readonly ShopDbContext _dbContext;
    private readonly Persistence.Interfaces.Contracts.Product _product;
    private HttpResponseMessage _httpResponse;

    private static IServiceProvider ServiceProvider => ServiceWebApplicationFactory.Instance.Services;
    private static HttpClient Client => ServiceWebApplicationFactory.Instance.CreateClient();

    public Get_Product_By_Id_Feature()
    {
        _dbContext = ServiceWebApplicationFactory.Instance.GetDbContext();
        _organisationId = Guid.NewGuid();
        _requestId = Guid.NewGuid();
        _productId = Guid.NewGuid();

        var fixture = new Fixture();

        _product = fixture.Build<Persistence.Interfaces.Contracts.Product>()
            .With(x => x.Id, _productId)
            .Create();
    }

    private async Task Product_exists_in_the_database()
    {
        _dbContext.Products.Add(_product);
        await _dbContext.SaveChangesAsync();
    }

    private async Task Get_product_by_id_endpoint_is_called()
    {
        _httpResponse = await Client.GetAsync($"/products/{_productId}");
    }

    private async Task Product_is_returned()
    {
        var productResponse = await _httpResponse.Content.ReadFromJsonAsync<ProductResponse>();
        productResponse.ShouldNotBeNull();
        productResponse!.Name.ShouldBe(_product.Name);
    }
}
