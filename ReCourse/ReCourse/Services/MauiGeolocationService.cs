using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors; // Perlu untuk mengakses API geolokasi MAUI
using ReCourse.Shared.Services;

namespace ReCourse.Services
{
    public class MauiGeolocationService : IGeolocationService
    {
        public async Task<LocationData> GetCurrentLocationAsync()
        {
            try
            {
                // 1. Cek dan minta izin (MAUI otomatis menangani permissions Android/iOS)
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
                if (status != PermissionStatus.Granted)
                {
                    return new LocationData { ErrorMessage = " Akses lokasi ditolak." };
                }
                
                // 2. Mendapatkan lokasi aktual saat ini
                var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));
                if (location != null)
                {
                    return new LocationData
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Altitude = location.Altitude
                    };
                }
            }
            catch (FeatureNotSupportedException)
            {
                return new LocationData { ErrorMessage = "Fitur Geolocation tidak didukung pada perangkat ini." };
            }
            catch (PermissionException)
            {
                return new LocationData { ErrorMessage = "Izin lokasi tidak diberikan." };
            }
            catch (Exception ex)
            {
                return new LocationData { ErrorMessage = $"Terjadi kesalahan saat mendapatkan lokasi: {ex.Message}" };
            }

            return new LocationData { ErrorMessage = "Lokasi tidak ditemukan." };
        }
    }
}
