using ProfileAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProfileAPI.Helpers;

namespace ProfileAPI.Services
{
    public class CountryService
    {

        //returns countries object 
        public async Task<List<Country>> LoadCountriesAsync(string path = "CountryCodes.json")
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
            options.Converters.Add(new CountryConverter());

            // Get the absolute path of the JSON file
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);

            using var fileStream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
            return await JsonSerializer.DeserializeAsync<List<Country>>(fileStream, options);
        }
    }
}
