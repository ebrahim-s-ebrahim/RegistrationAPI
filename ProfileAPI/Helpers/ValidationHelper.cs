using ProfileAPI.Models;
using System.Linq;

namespace ProfileAPI.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidCountry(string countryName, List<Country> countries)
        {
            var country = countries.FirstOrDefault(c => c.Name == countryName);
            return country != null;
        }

        public static bool IsValidDialCode(string dialCode, List<Country> countries)
        {
            var country = countries.FirstOrDefault(c => c.DialCode == dialCode);
            return country != null;
        }
    }
}
