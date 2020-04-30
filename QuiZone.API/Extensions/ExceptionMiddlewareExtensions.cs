using Microsoft.AspNetCore.Builder;
using QuiZone.API.Middleware;


namespace QuiZone.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
