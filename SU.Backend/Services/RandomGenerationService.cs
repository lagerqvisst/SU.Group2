using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SU.Backend.Helper;
using SU.Backend.Models;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Services
{
    public class RandomGenerationService : IRandomGenerationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;
        private readonly ILogger<RandomGenerationService> _logger;


        public RandomGenerationService(HttpClient httpClient, IConfiguration configuration, ILogger<RandomGenerationService> logger)
        {
            _httpClient = httpClient;
            _apiToken = configuration["ApiSettings:RandomApiKey"];
            _logger = logger;
        }

        public async Task<(bool Success, RandomUserApiResponse RandomUserInfo)> GenerateRandomCustomer()
        {
            try
            {
                _logger.LogInformation("Sending request to API with token: {ApiToken}", _apiToken);
                var response = await _httpClient.GetAsync($"https://randomuser.me/api/?apiKey={_apiToken}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Received successful response from API.");
                    var json = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug("Response content: {JsonContent}", json);

                    var apiResult = JsonSerializer.Deserialize<RandomUserApiResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResult != null && apiResult.Results.Count > 0)
                    {
                        var firstResult = apiResult.Results[0];
                    }

                    return (true, apiResult);
                }

                else
                {
                    _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
                    return (false, null);
                }
            }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "JSON deserialization error occurred while generating a random customer.");
                    return (false, null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while generating a random customer.");
                    return (false, null);
                }
        }


    }
}
