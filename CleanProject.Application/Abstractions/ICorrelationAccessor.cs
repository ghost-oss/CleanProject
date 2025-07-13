namespace CleanProject.Application.Abstractions;

public interface ICorrelationAccessor
{
    string GetCorrelationId();
}