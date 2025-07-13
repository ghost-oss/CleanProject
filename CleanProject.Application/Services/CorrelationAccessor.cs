using CleanProject.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CleanProject.Application.Services;

public class CorrelationAccessor : ICorrelationAccessor
{
    private readonly IHttpContextAccessor accessor;

    public CorrelationAccessor(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    public string GetCorrelationId() => accessor.HttpContext.Items["X-Correlation-ID"]?.ToString();
}