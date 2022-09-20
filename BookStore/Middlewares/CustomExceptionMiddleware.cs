using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace BookStore.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _requestDelegate = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP" + context.Request.Method + "-" + context.Request.Path;
                Console.WriteLine(message);

                await _requestDelegate(context);
                watch.Stop();

                message = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + "responded" + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
                Console.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }           
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
           string message = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + "responded" + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
