using CleanProject.Application.Abstractions;
using CleanProject.Application.Behaviours;
using CleanProject.Application.Services;
using CleanProject.Domain.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace CleanProject.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            //Register Mediator RequestHandlers
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly);
                config.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
            });

            //Register all concrete implementation of type IValidator<T> within this assembly
            services.AddValidatorsFromAssemblyContaining(typeof(DependancyInjection));

            //Register generic validator 
            services.AddScoped(typeof(ICommonValidator<>), typeof(Validator<>));
            
            services.AddHttpContextAccessor();
            services.AddScoped<ICorrelationAccessor, CorrelationAccessor>();

            return services;
        }
    }
}