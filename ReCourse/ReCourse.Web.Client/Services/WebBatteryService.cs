using ReCourse.Shared.Services;
using System;
using System.Threading.Tasks;

namespace ReCourse.Web.Client.Services
{
    // Placeholder untuk lingkungan web
    // Web tidak dapat secara native memonitor baterai dengan cara yang sama seperti MAUI
    public class WebBatteryService : IBatteryService
    {
        public event EventHandler<BatteryData> BatteryChanged;
        public BatteryData CurrentBatteryInfo => new BatteryData
        {
            ChargeLevel = 1.0, // Asumsi selalu penuh atau terhubung ke daya
            State = "Connected",
            Source = "AC/Web Host",
            StatusMessage = "Informasi baterai tidak tersedia di Web."
        };

        public Task<BatteryData> LoadBatteryInfoAsync()
        {
            return Task.FromResult(CurrentBatteryInfo);
        }
    }
}
