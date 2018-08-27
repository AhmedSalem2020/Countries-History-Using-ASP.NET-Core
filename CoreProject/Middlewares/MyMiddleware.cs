using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreProject.Middlewares
{
    public class MyMiddleware
    {
        RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("My Middleware");
            await _next.Invoke(context);

            await context.Response.WriteAsync("MyMiddleare After");
        }
    }
}
