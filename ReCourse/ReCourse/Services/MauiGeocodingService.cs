
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReCourse.Shared.Services;
using System.Net.Http.Json;

namespace ReCourse.Services
{
    public class MauiGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public MauiGeocodingService()
        {
            _httpClient = new HttpClient();
            // User-Agent wajib diisi untuk OpenStreetMap/Nominatim
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ReCourseApp");
        }
        public async Task<string> GetAddressAsync(double latitude, double longitude)
        {
            try
            {
                // Menggunakan API OpenStreetMap gratis
                var url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}";
                var response = await _httpClient.GetFromJsonAsync<NominatimResponse>(url);

                return response?.display_name ?? "Address not found.";
            }
            catch (Exception ex)
            {
                return $"Geocoding Error: {ex.Message}";
            }
        }

        // Helper class untuk menampung respons JSON
        private class NominatimResponse
        {
            public string display_name { get; set; }
        }
    }
}