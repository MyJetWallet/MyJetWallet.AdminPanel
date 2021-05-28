using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backoffice.Middlewares
{
    public class ApiTraceMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ApiTraceMiddleware> _logger;

        public ApiTraceMiddleware(RequestDelegate next, ILogger<ApiTraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            var method = context.Request.Method;
            var schema = context.Request.Scheme;

            if (!path.Contains("isalive") && !path.Contains("metrics"))
            {
                var user = context.Request.Headers.TryGetValue("x-ssl-user", out var sslCn)
                    ? sslCn.ToString()
                    : "no user";
                        
                Console.WriteLine($"Http call {schema} {method} {path} | {user}");

                await _next.Invoke(context);

                Console.WriteLine($"Http call DONE {schema} {method} {path}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}