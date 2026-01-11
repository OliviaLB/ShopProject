using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ServiceTests.CommonSteps;

public static class HttpResponse
{
    public static Task Status_code_is(
        HttpResponseMessage responseMessage,
        HttpStatusCode statusCode)
    {
        responseMessage.ShouldNotBeNull();
        responseMessage.StatusCode.ShouldBe(statusCode);

        return Task.CompletedTask;
    }

    public static async Task Content_Should_Not_Contain(
        HttpResponseMessage responseMessage,
        string expectedMessage)
    {
        var content = await responseMessage.Content.ReadAsStringAsync();
        Regex.Unescape(content).ShouldNotContain(expectedMessage);
    }

    public static async Task Is_Problem_Details_With(
        HttpResponseMessage response,
        string expectedMessage)
    {
        response.ShouldNotBeNull();
        response.Content.ShouldNotBeNull();

        var json = await response.Content.ReadAsStringAsync();

        json.ShouldNotBeNullOrWhiteSpace();

        var problem = JsonSerializer.Deserialize<ProblemDetails>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        problem.ShouldNotBeNull();
        problem!.Detail.ShouldBe(expectedMessage);
    }
}
