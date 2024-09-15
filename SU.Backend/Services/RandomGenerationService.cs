using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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


        public class RandomUserApiResponse
        {
            public List<Result> Results { get; set; }
            public Info Info { get; set; }
        }

        public class Result
        {
            public Name Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
            public Login Login { get; set; }
            public DateOfBirth Dob { get; set; }
            public Registered Registered { get; set; }
            public string Gender { get; set; }
            public Picture Picture { get; set; }
            public string Nat { get; set; }
        }

        public class Name
        {
            public string Title { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
        }

        public class Location
        {
            public Street Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public int Postcode { get; set; }
            public Coordinates Coordinates { get; set; }
            public Timezone Timezone { get; set; }
        }

        public class Street
        {
            public int Number { get; set; }
            public string Name { get; set; }
        }

        public class Coordinates
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }

        public class Timezone
        {
            public string Offset { get; set; }
            public string Description { get; set; }
        }

        public class Login
        {
            public string Uuid { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Salt { get; set; }
            public string Md5 { get; set; }
            public string Sha1 { get; set; }
            public string Sha256 { get; set; }
        }

        public class DateOfBirth
        {
            public DateTime Date { get; set; }
            public int Age { get; set; }
        }

        public class Registered
        {
            public DateTime Date { get; set; }
            public int Age { get; set; }
        }

        public class Picture
        {
            public string Large { get; set; }
            public string Medium { get; set; }
            public string Thumbnail { get; set; }
        }

        public class Info
        {
            public string Seed { get; set; }
            public int Results { get; set; }
            public int Page { get; set; }
            public string Version { get; set; }
        }
    }
}
