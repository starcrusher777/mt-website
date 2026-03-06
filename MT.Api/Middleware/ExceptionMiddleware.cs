using System.Net;
using System.Text.Json;
using FluentValidation;
using MT.Contracts.Common;
using MT.Domain.Exceptions;

namespace MerchTrade.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            if (ex is not ValidationException)
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var response = new ApiErrorResponse
            {
                Errors = validationException.Errors
                    .Select(e => new ErrorDetail { Code = e.ErrorCode, Message = e.ErrorMessage })
                    .ToList()
            };
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            return;
        }

        var (statusCode, code, message) = exception switch
        {
            NotFoundException nf => (HttpStatusCode.NotFound, "NOT_FOUND", nf.Message),
            UnauthorizedAccessException ua => (HttpStatusCode.Unauthorized, "UNAUTHORIZED", ua.Message),
            ArgumentException arg => (HttpStatusCode.BadRequest, "BAD_REQUEST", arg.Message),
            _ => (HttpStatusCode.InternalServerError, "INTERNAL_ERROR", "An internal error occurred.")
        };

        context.Response.StatusCode = (int)statusCode;
        var errorResponse = new ApiErrorResponse
        {
            Errors = new List<ErrorDetail> { new() { Code = code, Message = message } }
        };
        var opts = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, opts));
    }
}
