using Microsoft.Extensions.Configuration;

namespace Weather
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var repository = new WeatherRepository(config);

            Console.WriteLine("Welcome to the Open Weather API!");

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Get weather for a specific city");
                Console.WriteLine("2. Exit");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Enter the city name:");
                    string cityName = Console.ReadLine();

                    Console.WriteLine("Enter the country code (optional, e.g., PK):");
                    string countryCode = Console.ReadLine();

                    if (string.IsNullOrEmpty(countryCode))
                    {
                        repository.GetWeatherForCity(cityName);
                    }
                    else
                    {
                        repository.GetWeatherForCity(cityName, countryCode);
                    }
                }
                else if (choice == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
        }
   
    }
}
