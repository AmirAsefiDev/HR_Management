using System.Text.Json;
using HR_Management.Common;

namespace HR_Management.Api.Middleware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        //if token isn't valid
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ResultDto
            {
                IsSuccess = false,
                Message = "Authentication failed. Please login again.",
                StatusCode = 401
            }));
        }

        //if user has login but doesn't have permission
        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ResultDto
            {
                Message = "You don't have permission to access this resource.",
                StatusCode = 403,
                IsSuccess = false
            }));
        }
    }
}