using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.TransactionEntities.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException e)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://httpstatuses.com/401",
                Detail = e.Message
            };
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception e)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://httpstatuses.com/500"
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
