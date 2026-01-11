using API.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occured, please try again later.";

        switch (exception)
        {
            case ProductValidationException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case ProductNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case UniqueProductException:
                statusCode = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
        }

        var problemDetails = new ProblemDetails { Detail = message, Status = (int)statusCode };
        httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
