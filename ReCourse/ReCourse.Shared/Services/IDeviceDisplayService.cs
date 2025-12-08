//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ReCourse.Shared.Services
//{
//    public class DisplayData
//    {
//        public double Width { get; set; } // Lebar layar dalam piksel
//        public double Height { get; set; } // Tinggi layar dalam piksel
//        public double Density { get; set; } // Kepadatan piksel (DPI)
//        public string Orientation { get; set; } = "Unknown"; // Orientasi layar (Portrait/Landscape)
//        public bool IsScreenOn { get; set; } = false; // Status layar tetap menyala

//        // Interface untuk service Tampilan Perangkat
//        public interface IDeviceDisplayService
//        {
//            // Event yang dipicu saat konfigurasi layar berubah (orientasi, dll)
//            event EventHandler<DisplayData> MainDisplayInfoChanged;

//            // Mendapatkan data tampilan saat ini
//            DisplayData CurrentDisplayInfo { get; }
            
//            // Mengaktifkan/menonaktifkan agar layar tetap menyala (Keep Screen On)
//            void SetScreenLock(bool keepScreenOn);
//        }
//    }
//}
