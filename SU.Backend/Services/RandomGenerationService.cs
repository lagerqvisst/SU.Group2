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

        public async Task<(bool Success, RandomUserApiResponse RandomUserInfo)> GenerateSingleRandomUser()
        {
            //Lite problem ibland när man anropar API:et, så vi gör ett antal försök
            const int maxRetries = 3; // Max antal försök
            int retryCount = 0;

            while (retryCount < maxRetries)
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
                            return (true, apiResult);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
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

                retryCount++;
                _logger.LogInformation("Retrying... Attempt {Attempt}/{MaxAttempts}", retryCount, maxRetries);
                await Task.Delay(1000); // Vänta en sekund innan nästa försök
            }

            _logger.LogError("Max retries reached. Failed to generate a random customer.");
            return (false, null);
        }


        /// <summary>
        /// If we want to have multiple random users, for eg. to quickly seed DB with test data. 
        /// </summary>
        public async Task<(bool Success, List<RandomUserApiResponse> RandomUserInfo)> GenerateMultipleRandomUsers(int numberOfUsers = 10)
        {
            try
            {
                _logger.LogInformation("Sending request to API to generate {NumberOfUsers} random users with token: {ApiToken}", numberOfUsers, _apiToken);
                var response = await _httpClient.GetAsync($"https://randomuser.me/api/?results={numberOfUsers}&apiKey={_apiToken}");

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
                        // Mappa om Result-objekten till RandomUserApiResponse
                        var randomUsers = apiResult.Results.Select(r => new RandomUserApiResponse
                        {
                            // Fyll i egenskaper från r till en ny instans av RandomUserApiResponse
                        }).ToList();

                        _logger.LogInformation("Successfully parsed {Count} random users.", randomUsers.Count);
                        return (true, randomUsers); // Returnerar mappade användare i listan
                    }

                    _logger.LogWarning("No users were found in the API response.");
                    return (false, null);
                }
                else
                {
                    _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
                    return (false, null);
                }
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "JSON deserialization error occurred while generating random users.");
                return (false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating random users.");
                return (false, null);
            }
        }




    }
}
