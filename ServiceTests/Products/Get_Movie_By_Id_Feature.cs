using ServiceTests.CommonSteps;
using System.Net;

namespace ServiceTests.Products;

public partial class Get_Product_By_Id_Feature
{
    [Scenario]
    public async Task Get_Product_By_Id_Returns_Product()
    {
        await Runner.RunScenarioAsync(
            given => Product_exists_in_the_database(),
            when => Get_product_by_id_endpoint_is_called(),
            then => HttpResponse.Status_code_is(_httpResponse, HttpStatusCode.OK),
            and => Product_is_returned());
    }

    [Scenario]
    public async Task Get_Product_By_Id_Returns_Not_Found_When_Product_Not_Found()
    {
        await Runner.RunScenarioAsync(
            given => Get_product_by_id_endpoint_is_called(),
            then => HttpResponse.Status_code_is(_httpResponse, HttpStatusCode.NotFound),
            and => HttpResponse.Is_Problem_Details_With(_httpResponse, ExceptionMessages.ProductNotFound(_productId)));
    }
}