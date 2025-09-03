using HR_Management.Common;

namespace HR_Management.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var response = ResultDto.Failure("خطای داخلی سرور رخ داده است.", 500);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}