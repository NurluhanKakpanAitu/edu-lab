
using System.Text.Json;
using Application;

namespace WebAPI
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continue processing the request
                await next(context);
            }
            catch (Exception ex)
            {
                // Handle exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine($"[Exception]: {exception.Message}");
            
            var result = JsonSerializer.Serialize(ApiResponse<object>.CreateFailure(exception.Message), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;

            return context.Response.WriteAsync(result);
        }
    }
}