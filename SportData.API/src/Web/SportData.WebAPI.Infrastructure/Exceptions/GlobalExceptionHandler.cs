namespace SportData.WebAPI.Infrastructure.Exceptions;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Guid.NewGuid();
        this.logger.LogError($"Error occure while processing the request, TraceId : {traceId}, Message : {exception.Message}, StackTrace: {exception.StackTrace}");

        var problemDetails = new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Instance = httpContext.Request.Path,
            Detail = $"Internal server error occured, traceId : {traceId}",
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        //switch (exception)
        //{
        //    case BadHttpRequestException:
        //        problemDetails.Status = StatusCodes.Status400BadRequest;
        //        problemDetails.Title = "Bad Request";
        //        break;
        //    default:
        //        problemDetails.Status = StatusCodes.Status500InternalServerError;
        //        problemDetails.Title = "Internal Server Error";
        //        break;
        //}

        //httpContext.Response.StatusCode = problemDetails.Status.Value;
        //await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}