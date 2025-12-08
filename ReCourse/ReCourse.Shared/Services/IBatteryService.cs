using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class BatteryData
    {
        public double ChargeLevel { get; set; } = 0; // Level baterai (0.00 hingga 1.00)
        public string State { get; set; } = "Unknown"; // Status pengisian baterai
        public string Source { get; set; } = "Unknown"; // Sumber daya baterai
        public string StatusMessage { get; set; } = "Not Available"; 
    }
    // Interface untuk service Baterai
    public interface IBatteryService
    {
        // Event yang dipicu saat level atau status baterai berubah
        event EventHandler<BatteryData> BatteryChanged;

        // Mendapatkan status baterai saat ini secara instan
        BatteryData CurrentBatteryInfo { get; }

        // Memuat status baterai saat ini
        Task<BatteryData> LoadBatteryInfoAsync();
    }
}
