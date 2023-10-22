using System;
using System.Reflection;
using FluentValidation;
using CleanProject.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using CleanProject.Application.Features.Employees.Commands;
using CleanProject.Domain.Validator;
namespace CleanProject.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            //Register all concrete implementation of type IValidator<T> within this assembly
            services.AddValidatorsFromAssemblyContaining(typeof(DependancyInjection));

            //Register generic validator 
            services.AddScoped(typeof(Domain.Validator.IValidator<>), typeof(Validator<>));

            return services;
        }
    }
}