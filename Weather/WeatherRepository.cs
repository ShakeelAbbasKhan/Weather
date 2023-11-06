using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Weather
{
    public class WeatherRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public WeatherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
        }


        public void GetWeatherForCity(string city, string countryCode = "PK")
        {
            string apiKey = _configuration["WeatherApi:ApiKey"];
            string baseUrl = _configuration["WeatherApi:BaseUrl"];

            var userURL = $"{baseUrl}weather?q={city},{countryCode}&appid={apiKey}&units=metric";

            try
            {
                var response = _client.GetAsync(userURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var weatherResponse = response.Content.ReadAsStringAsync().Result;
                    var temperature = JObject.Parse(weatherResponse);
                    var formattedResponseMain = temperature["main"]["temp"].ToString();
                    Console.WriteLine($"\n{city}, {countryCode}: {formattedResponseMain} degrees Centigrade.");
                }
                else
                {
                    Console.WriteLine($"Error: City '{city}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
