using AutoFixture;
using Domain.Mapper;
using Persistence.Interfaces.Contracts;
using Shouldly;

namespace UnitTests.Mapping;

public class ProductResponseMapperTests
{
    private readonly IFixture _fixture;
    private readonly Mapper _sut;

    public ProductResponseMapperTests()
    {
        _fixture = new Fixture();
        _sut = new Mapper();
    }

    [Fact]
    public void Map_To_Response_Product_Should_Map_All_Properties()
    {
        var product = _fixture.Create<Product>();

        var result = _sut.MapToResponse(product);

        result.Id.ShouldBe(product.Id);
        result.Name.ShouldBe(product.Name);
        result.Description.ShouldBe(product.Description);
        result.Price.ShouldBe(product.Price);
        result.PictureUri.ShouldBe(product.PictureUri);
        result.Type.ShouldBe(product.Type);
        result.Brand.ShouldBe(product.Brand);
        result.QuantityInStock.ShouldBe(product.QuantityInStock);
        result.ChangeTimestamp.ShouldBe(product.ChangeTimestamp);
        result.DateAdded.ShouldBe(product.DateAdded);
    }

    [Fact]
    public void Map_To_Response_ProductList_Should_Map_All_Items()
    {
        var products = _fixture.CreateMany<Product>(5).ToList();

        var result = _sut.MapToResponse(products);

        result.Count.ShouldBe(products.Count);

        for (var i = 0; i < products.Count; i++)
        {
            result[i].Id.ShouldBe(products[i].Id);
            result[i].Name.ShouldBe(products[i].Name);
            result[i].Description.ShouldBe(products[i].Description);
            result[i].Price.ShouldBe(products[i].Price);
            result[i].PictureUri.ShouldBe(products[i].PictureUri);
            result[i].Type.ShouldBe(products[i].Type);
            result[i].Brand.ShouldBe(products[i].Brand);
            result[i].QuantityInStock.ShouldBe(products[i].QuantityInStock);
            result[i].ChangeTimestamp.ShouldBe(products[i].ChangeTimestamp);
            result[i].DateAdded.ShouldBe(products[i].DateAdded);
        }
    }

    [Fact]
    public void Map_To_Response_Pagination_Should_Map_All_Properties()
    {
        var pagination = _fixture.Create<Pagination>();

        var result = _sut.MapToResponse(pagination);

        result.PageNumber.ShouldBe(pagination.PageNumber);
        result.PageSize.ShouldBe(pagination.PageSize);
        result.TotalItems.ShouldBe(pagination.TotalItems);
        result.TotalPages.ShouldBe(pagination.TotalPages);
    }

    [Fact]
    public void Map_To_Response_PagedList_Should_Map_Items_And_Pagination()
    {
        var products = _fixture.CreateMany<Product>(4).ToList();
        var pagination = _fixture.Create<Pagination>();

        var pagedProducts = new PagedList<Product>
        {
            Items = products,
            Pagination = pagination
        };

        var result = _sut.MapToResponse(pagedProducts);

        result.Pagination.PageNumber.ShouldBe(pagination.PageNumber);
        result.Pagination.PageSize.ShouldBe(pagination.PageSize);
        result.Pagination.TotalItems.ShouldBe(pagination.TotalItems);
        result.Pagination.TotalPages.ShouldBe(pagination.TotalPages);

        result.Items.Count.ShouldBe(products.Count);

        for (var i = 0; i < products.Count; i++)
        {
            result.Items[i].Id.ShouldBe(products[i].Id);
            result.Items[i].Name.ShouldBe(products[i].Name);
            result.Items[i].Description.ShouldBe(products[i].Description);
            result.Items[i].Price.ShouldBe(products[i].Price);
            result.Items[i].PictureUri.ShouldBe(products[i].PictureUri);
            result.Items[i].Type.ShouldBe(products[i].Type);
            result.Items[i].Brand.ShouldBe(products[i].Brand);
            result.Items[i].QuantityInStock.ShouldBe(products[i].QuantityInStock);
            result.Items[i].ChangeTimestamp.ShouldBe(products[i].ChangeTimestamp);
            result.Items[i].DateAdded.ShouldBe(products[i].DateAdded);
        }
    }

    [Fact]
    public void Map_To_Response_ProductList_WhenEmpty_Should_Return_Empty_List()
    {
        List<Product> products = [];

        var result = _sut.MapToResponse(products);

        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}
