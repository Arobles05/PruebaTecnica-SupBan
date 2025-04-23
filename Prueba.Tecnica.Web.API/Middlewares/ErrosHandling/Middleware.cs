using Prueba.Tecnica.Web.Application.ResponseModels;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Prueba.Tecnica.Web.API.Middlewares.ErrosHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadHttpRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HandleExceptionAsync(context, ex);

            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HandleExceptionAsync(context, ex);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                  await HandleExceptionAsync(context, ex);

            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = ApiResponse<object>.CreateErrorResponse("Internal Server Error");
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
