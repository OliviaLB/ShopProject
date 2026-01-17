using AutoFixture;
using Domain.Mapper;
using Shouldly;

using FiltersContract = Contracts.Filters;
using PersistanceContract = Persistence.Interfaces.Contracts;
using PersistanceFilters = Persistence.Interfaces.Contracts.Filters;
using RequestContract = Contracts.Requests;

namespace UnitTests.Mapping;

public class ProductPersistenceMapperTests
{
    private readonly IFixture _fixture;
    private readonly Mapper _sut;

    public ProductPersistenceMapperTests()
    {
        _fixture = new Fixture();
        _sut = new Mapper();
    }

    [Fact]
    public void Map_To_Persistence_PaginationFilter_Should_Map_All_Properties()
    {
        var contract = _fixture.Build<FiltersContract.PaginationFilter>()
            .With(x => x.SortDirection, FiltersContract.SortDirection.Asc)
            .Create();

        var result = _sut.MapToPersistence(contract);

        result.PageNumber.ShouldBe(contract.PageNumber);
        result.PageSize.ShouldBe(contract.PageSize);
        result.SortField.ShouldBe(contract.SortField);
        result.SortDirection.ShouldBe(PersistanceFilters.SortDirection.Asc);
    }

    [Theory]
    [InlineData(FiltersContract.SortDirection.Asc, PersistanceFilters.SortDirection.Asc)]
    [InlineData(FiltersContract.SortDirection.Desc, PersistanceFilters.SortDirection.Desc)]
    public void Map_To_Persistence_SortDirection_ShouldMapCorrectly(
        FiltersContract.SortDirection input,
        PersistanceFilters.SortDirection expected)
    {
        var result = _sut.MapToPersistence(input);

        result.ShouldBe(expected);
    }

    [Fact]
    public void Map_To_Persistence_SortDirection_Unsupported_Should_Throw_Argument_Out_Of_Range_Exception()
    {
        var unsupported = (FiltersContract.SortDirection)999;

        var ex = Should.Throw<ArgumentOutOfRangeException>(() => _sut.MapToPersistence(unsupported));

        ex.ParamName.ShouldBe("sort");
    }

    [Fact]
    public void Map_To_Persistence_GetProductFilters_Should_Map_All_Properties()
    {
        var contract = _fixture.Create<FiltersContract.GetProductFilters>();

        var result = _sut.MapToPersistence(contract);

        result.Ids.ShouldBe(contract.Ids);
        result.Brands.ShouldBe(contract.Brands);
        result.Types.ShouldBe(contract.Types);
        result.InStockOnly.ShouldBe(contract.InStockOnly);
        result.SearchTerm.ShouldBe(contract.SearchTerm);
    }

    [Fact]
    public void Map_To_Persistence_CreateProductRequest_Should_Create_New_Product_And_Map_Fields()
    {
        var request = _fixture.Create<RequestContract.CreateProductRequest>();

        var result = _sut.MapToPersistence(request);

        result.Name.ShouldBe(request.Name);
        result.Description.ShouldBe(request.Description);
        result.Price.ShouldBe(request.Price);
        result.PictureUri.ShouldBe(request.PictureUri);
        result.Type.ShouldBe(request.Type);
        result.Brand.ShouldBe(request.Brand);
        result.QuantityInStock.ShouldBe(request.QuantityInStock);

        result.Id.ShouldNotBe(Guid.Empty);

        result.DateAdded.ShouldBeGreaterThan(DateTime.Now.AddMinutes(-1));
        result.DateAdded.ShouldBeLessThanOrEqualTo(DateTime.Now.AddMinutes(1));
    }

    [Fact]
    public void Map_To_Persistence_UpdateProductRequest_When_Normalised_Name_Provided_Should_Use_NormalisedName()
    {
        var request = _fixture.Create<RequestContract.UpdateProductRequest>();
        var existing = _fixture.Create<PersistanceContract.Product>();
        var normalisedName = _fixture.Create<string>();

        var result = _sut.MapToPersistence(request, existing, normalisedName);

        result.Id.ShouldBe(request.Id);
        result.Name.ShouldBe(normalisedName);
        result.ChangeTimestamp.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-1));
        result.ChangeTimestamp.ShouldBeLessThanOrEqualTo(DateTime.UtcNow.AddMinutes(1));
    }

    [Fact]
    public void Map_To_Persistence_UpdateProductRequest_When_Normalised_Name_Null_Should_Fallback_To_Existing_Name()
    {
        var request = _fixture.Create<RequestContract.UpdateProductRequest>();
        var existing = _fixture.Create<PersistanceContract.Product>();

        var result = _sut.MapToPersistence(request, existing, normalisedName: null);

        result.Name.ShouldBe(existing.Name);
    }

    [Fact]
    public void Map_To_Persistence_UpdateProductRequest_When_Request_Fields_Null_Should_Fallback_To_Existing_Product_Fields()
    {
        var request = _fixture.Build<RequestContract.UpdateProductRequest>()
            .With(x => x.Description, (string?)null)
            .With(x => x.Price, (decimal?)null)
            .With(x => x.PictureUri, (string?)null)
            .With(x => x.Type, (string?)null)
            .With(x => x.Brand, (string?)null)
            .With(x => x.QuantityInStock, (int?)null)
            .Create();

        var existing = _fixture.Create<PersistanceContract.Product>();

        var result = _sut.MapToPersistence(request, existing, normalisedName: null);

        result.Description.ShouldBe(existing.Description);
        result.Price.ShouldBe(existing.Price);
        result.PictureUri.ShouldBe(existing.PictureUri);
        result.Type.ShouldBe(existing.Type);
        result.Brand.ShouldBe(existing.Brand);
        result.QuantityInStock.ShouldBe(existing.QuantityInStock);

        result.Id.ShouldBe(request.Id);
        result.ChangeTimestamp.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-1));
        result.ChangeTimestamp.ShouldBeLessThanOrEqualTo(DateTime.UtcNow.AddMinutes(1));
    }

    [Fact]
    public void Map_To_Persistence_UpdateProductRequest_When_Request_Fields_Provided_Should_Use_Request_Fields()
    {
        var request = _fixture.Build<RequestContract.UpdateProductRequest>()
            .With(x => x.Description, _fixture.Create<string>())
            .With(x => x.Price, _fixture.Create<long>())
            .With(x => x.PictureUri, _fixture.Create<string>())
            .With(x => x.Type, _fixture.Create<string>())
            .With(x => x.Brand, _fixture.Create<string>())
            .With(x => x.QuantityInStock, _fixture.Create<int>())
            .Create();

        var existing = _fixture.Create<PersistanceContract.Product>();

        var result = _sut.MapToPersistence(request, existing, normalisedName: null);

        result.Description.ShouldBe(request.Description);
        result.Price.ShouldBe(request.Price!.Value);
        result.PictureUri.ShouldBe(request.PictureUri);
        result.Type.ShouldBe(request.Type);
        result.Brand.ShouldBe(request.Brand);
        result.QuantityInStock.ShouldBe(request.QuantityInStock!.Value);

        result.Id.ShouldBe(request.Id);
    }
}