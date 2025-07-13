using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace CleanProject.Application.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate next;
    private const string CorrelationHeader = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var correlationId = string.Empty;
        
        if (!context.Request.Headers.ContainsKey(CorrelationHeader))
        {
            correlationId = Guid.NewGuid().ToString();
            
            //Don't store the correlationId in the request headers, it's meant to be immutable so store it in the items where it's appropriate to add/read/modify context items across services etc
            //context.Request.Headers[CorrelationHeader] = correlationId;
            
            // Store in HttpContext.Items for easy access in app
            context.Items[CorrelationHeader] = correlationId;
        }
        
        //Response headers are immutable once the response creation has started. The delegate ensures you can write to the response headers just before it locks
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationHeader] = correlationId;
            return Task.CompletedTask;
        });

        using (LogContext.PushProperty(CorrelationHeader, correlationId))
        {
            await next(context);
        }
    }
}