using CleanProject.API.Resilience;
using Polly;

namespace CleanProject.API
{
    public static class DependancyInjection
	{
		public static IServiceCollection RegisterApi(this IServiceCollection services)
		{
            //Adds Named client
            services.AddHttpClient("CleanProjectClient", httpclient =>
            {
                httpclient.BaseAddress = new Uri("http://localhost:5236");
            })
            .AddResilienceHandler("http-client-policy", builder =>
            {
                //Add retry strategy options
                builder.AddRetry(PollyHttpRetryStrategyOptions.GetHttpRetryOptions());

                //Add timeout in case operation spans over 10 seconds and prevent log hangs.
                //This timeout applies to the HTTP request exection only NOT the entire retry sequence time
                builder.AddTimeout(TimeSpan.FromSeconds(10));
            });

            return services;
		}
	}
}

