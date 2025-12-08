using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class LocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
    public interface  IGeolocationService
    {
        Task<LocationData> GetCurrentLocationAsync();
    }
}
