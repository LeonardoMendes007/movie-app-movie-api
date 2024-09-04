using MovieApp.Domain.Exceptions;
using MovieApp.MovieApi.API.Response;
using System.Net;

namespace MovieApp.MovieApi.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogWarning("Resource not found");

            var response = ResponseBase.ResponseBaseFactory(HttpStatusCode.NotFound, ex.Message);

            await WriteResponseAsync<ResponseBase>(context, response, HttpStatusCode.NotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            var response = ResponseBase.ResponseBaseFactory(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.");

            await WriteResponseAsync<ResponseBase>(context, response, HttpStatusCode.InternalServerError);
        }
    }

    public async Task WriteResponseAsync<T>(HttpContext context, T response, HttpStatusCode httpStatusCode)
    {

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
