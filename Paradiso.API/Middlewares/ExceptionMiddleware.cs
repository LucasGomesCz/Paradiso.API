using System.Text.Json;

namespace Paradiso.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception error)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            if (error is ExceptionDto e)
            {
                response.StatusCode = 400;

                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = e.Message,
                    occurred_at = DateTime.UtcNow
                }));
            }
            else
            {
                response.StatusCode = 500;

                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = error.Message,
                    occurred_at = DateTime.UtcNow
                }));
            }
        }
    }
}
