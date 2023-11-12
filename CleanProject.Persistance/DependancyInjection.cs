using System;
using CleanProject.Application.Abstractions.Persistance;
using CleanProject.Persistance.Context;
using CleanProject.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanProject.Persistance
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEmployeeRepository, EmployeeRespository>();
            return services;
        }
    }
}

