using System.Text.Json;
using BusinessException = WorkSchedule.Application.Exceptions.BusinessException;

namespace WorkSchedule.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(httpContext, ex.ErrorMessages);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, new List<string> { ex.Message.Split("\r").First() }, 599);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, List<string> messages, int statusCode = 500)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var response = new ErrorResponse() { StatusCode = statusCode, Messages = messages };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
