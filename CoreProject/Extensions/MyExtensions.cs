using CoreProject.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MyExtensions
    {
        public static IApplicationBuilder UseAhmedSalem(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyMiddleware>();
            return app;
        }
    }
}
