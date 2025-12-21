using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class GeocodedAddress
    {
        public string Address { get; set; } = "N/A";
        public string Locality { get; set; } = "N/A"; // Kota/Kabupaten
        public string CountryName { get; set; } = "N/A";
        public string PostalCode { get; set; } = "N/A";
        public string ErrorMessage { get; set; } = string.Empty;
    }
    public interface IGeocodingService
    {
        Task<string> GetAddressAsync(double latitude, double longitude);
    }
}
