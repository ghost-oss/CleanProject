using System;
using CleanProject.Persistance.Context;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApplication.IntergrationTests
{
	public static class ApplicationDbContextSeederExtension
	{
		public static void EnsureCreatedAndSeed(this IServiceProvider serviceProvider, Action<ApplicationDbContext> action)
		{

            /*
			 * There are times when we need to resolve a scoped service, and initalise things prior to HTTP calls (which automaticaly creates a scope) 
			 * Scenario cases: 
			 *  Within program.cs (because it's during start up and no scope has been created i.e a HTTP request initiated)
			 *  During test initializtion (because it's during the test start up and no scope has been created)
			 *  Basically accessing a scoped service directly outside of a http request 
			 *  
			 *  
			 * When it's not needed?
			 *  Within a HTTPRequest
			 *  Within a HTTPRequest using HTTPClient created by WebApplicationFactory (Intergration testing)
			 *  Within middleware as that's a per request
			 */
            using (var scope = serviceProvider.CreateScope())
			{
				var scopedDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				//Ensures dbcontext is deleted with no cross test contamination
				scopedDbContext.Database.EnsureDeleted();
				//Ensures dbcontext is created, and all schemas are initalised (similar to what migrations would do) 
				scopedDbContext.Database.EnsureCreated();

				//Action delegate for anyone wanting to set up seedData for dbcontext
				action(scopedDbContext);
			}
		}
	}
}

