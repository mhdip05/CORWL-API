using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;

namespace CORWL_API.Middleware
{
    public class ApiVersioningMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiVersioningMiddleware(RequestDelegate next)
        {
            _next = next;
        }

#nullable disable
        public async Task InvokeAsync(HttpContext context)
        {
          
                var apiVersion = context.Request.Query["api-version"].ToString();
                if (string.IsNullOrEmpty(apiVersion))
                {
                    apiVersion = "1"; // Default to version 1 if no version specified
                }

                // Modify the request path to include the version prefix.
                context.Request.Path = $"/api/v{apiVersion}{context.Request.Path}";

                await _next(context);
            
        }
    }
}
