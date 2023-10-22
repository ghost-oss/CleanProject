using System;
using Microsoft.AspNetCore.Builder;

namespace CleanProject.Application.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void AddCustomMiddlewareExtensions(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

