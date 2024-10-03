using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ThreeLayer.MVC.Middleware
{
    public class ConcurrencyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConcurrencyMiddleware> _logger;
        public ConcurrencyMiddleware(RequestDelegate next, ILogger<ConcurrencyMiddleware> logger)
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
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency exception occurred.");

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "A concurrency conflict occurred. Your changes were not saved.",
                    details = ex.Message
                };

                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
