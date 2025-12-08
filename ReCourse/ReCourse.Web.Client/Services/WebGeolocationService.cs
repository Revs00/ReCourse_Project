using ReCourse.Shared.Services;
using System.Threading.Tasks;

namespace ReCourse.Web.Client.Services
{
    // Placeholder untuk layanan geolokasi di platform web
    // Di Web, kita bisa menggunakan browser Geolocation API via JavaScript interop, tapi untuk konsistensi, kita kembalikan lokasi default atau error
    public class WebGeolocationService : IGeolocationService
    {
        public Task<LocationData> GetCurrentLocationAsync()
        {
            // Mengembalikan lokasi default (misalnya, pusat Indonesia) atau error
            return Task.FromResult(new LocationData
            {
                Latitude = -6.2088, // Jakarta
                Longitude = 106.8456,
                ErrorMessage = "Menggunakan lokasi default (Web Mode)."
            });
        }
    }
}
