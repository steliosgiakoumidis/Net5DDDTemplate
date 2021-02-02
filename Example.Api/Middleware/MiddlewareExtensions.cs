using Microsoft.AspNetCore.Builder;

namespace Example.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseEnrichedLogs(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
        
        public static IApplicationBuilder UseCustomExceptionHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}