using Microsoft.Extensions.Configuration;
using VoteAPI.Application.Interfaces;

namespace VoteAPI.Application.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ExternalAPIService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<bool> CheckCPFIsValid(string cpf)
        {
            string apiUrl = Environment.GetEnvironmentVariable("CPFAPI") ?? _configuration.GetSection("APIs").GetSection("CPFUrl").Value;
            apiUrl = $"{apiUrl}/users/{cpf}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            bool isValid = bool.Parse(result);
            return isValid;
        }
    }
}
