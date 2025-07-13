using System;
using CleanProject.Domain.Interfaces;
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
            /*
             * EF Core fully intergrates with Microsoft.Extensions.Logging, and when calling AddDbContext automatically sets up logging via the normal .Net Mechanism.
             * This will mean it will look for Appsettings.Logging.Default and check for your log level and log in the output window
             * You can overide and choose where to log - https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
             * 
             */
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //Switch between the Defaults for container to container or localhost .net to sql server container
                options.UseSqlServer(configuration.GetConnectionString("DefaultLocalhostConnection"));
            });

            services.AddScoped<IEmployeeRepository, EmployeeRespository>();
            return services;
        }
    }
}

