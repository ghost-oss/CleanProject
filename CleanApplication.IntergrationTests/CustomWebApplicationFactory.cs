using CleanProject.Application.Abstractions;
using CleanProject.Persistance.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CleanApplication.IntergrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //When app is built, using app.IsDevelopment() will be set - note this does not set the environment variable for DOTNETCORE/ASPNETCORE Environment
            builder.UseEnvironment("Development");

            //This will set up your env varaibles
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            builder.ConfigureServices(services =>
            {
                services.Replace(ServiceDescriptor.Scoped<ICorrelationAccessor, MyAccessor>());


                services.Replace(ServiceDescriptor.Scoped<ApplicationDbContext>(service =>
                {
                    var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().
                    UseInMemoryDatabase("InMemoryDb")
                    .Options;

                    return new ApplicationDbContext(dbContextOptions);
                }));
            });

        }
    }

    public class MyAccessor : ICorrelationAccessor
    {
        public string GetCorrelationId()
        {
            return "CUSTOM TYPE OVERRIDED HAHA";
        }
    }
}

