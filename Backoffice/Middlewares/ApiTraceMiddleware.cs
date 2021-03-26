using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backoffice.Middlewares
{
    public class ApiTraceMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ApiTraceMiddleware> _logger;

        public ApiTraceMiddleware(RequestDelegate next, ILogger<ApiTraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            var schema = context.Request.Scheme;
            
            Console.WriteLine($"Http call {schema} {method} {path}");
            
            await _next.Invoke(context);
            
            Console.WriteLine($"Http call DONE {schema} {method} {path}");
        }
    }
}