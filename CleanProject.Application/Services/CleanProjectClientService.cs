using System;
namespace CleanProject.Application.Services
{
	public class CleanProjectClientService : ICleanProjectClientService
	{
		private readonly IHttpClientFactory httpClientFactory;

		public CleanProjectClientService(IHttpClientFactory httpClientfactory)
		{
			httpClientFactory = httpClientfactory;
		}

        public async Task<string> GetAsync()
        {
			var httpClient = httpClientFactory.CreateClient("CleanProjectClient");

			var response = await httpClient.GetAsync("test");

			response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

	public interface ICleanProjectClientService
	{
		Task<string> GetAsync();
	}
}

