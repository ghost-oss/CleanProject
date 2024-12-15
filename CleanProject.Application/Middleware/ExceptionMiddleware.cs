using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CleanProject.Application.Middleware
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
         {
                await ConvertException(httpContext, ex);
            }
        }

        public Task ConvertException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = ex.Message;

            if (ex is ValidationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var validationException = ex as ValidationException;
                result = GetValidationMessageString(validationException);
            }

            return httpContext.Response.WriteAsync(result);
        }

        public string GetValidationMessageString(ValidationException validationException)
        {
            var errors = validationException?.Errors.GroupBy(x => x.PropertyName)
                    .Select(g => new { FailedProperty = g.Key, Errors = g.Select(e => e.ErrorMessage) });

            return JsonConvert.SerializeObject(errors);
        }
    }
}

