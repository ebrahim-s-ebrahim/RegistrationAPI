using ProfileAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ProfileAPI.Helpers
{
    public class CountryConverter : JsonConverter<Country>
    {
        public static Country Convert(string countryName, string dialCode, List<Country> countries)
        {
            // Find the first country in the list that matches the country name or dial code.
            Country country = countries.FirstOrDefault(c => c.Name == countryName || c.DialCode == dialCode);

            // Return the country, or null if no matching country was found.
            return country;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Country);
        }

        public override Country Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var country = new Country();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return country;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "name":
                        country.Name = reader.GetString();
                        break;
                    case "dial_code":
                        country.DialCode = reader.GetString();
                        break;
                    case "code":
                        // ignore the code property
                        break;
                    default:
                        throw new JsonException($"Unexpected property '{propertyName}'");
                }
            }
            throw new JsonException("Unexpected end of JSON");
        }

        public override void Write(Utf8JsonWriter writer, Country value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

    }
}
