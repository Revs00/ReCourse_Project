//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Maui.Devices; // Perlu untuk mengakses API tampilan MAUI
//using ReCourse.Shared.Services;

//namespace ReCourse.Services
//{
//    public class MauiDeviceDisplay : IDeviceDisplay, IDisposable
//    {
//        private DisplayData _currentDisplayInfo = new DisplayData();
//        public event EventHandler<DisplayData> MainDisplayInfoChanged;
//        public DisplayData CurrentDisplayInfo => _currentDisplayInfo;
//        public MauiDeviceDisplayService()
//        {
//            // Langganan ke event bawaan MAUI
//            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
//            UpdateDisplayInfo();
//        }

//        private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
//        {
//            UpdateDisplayInfo();
//            MainDisplayInfoChanged?.Invoke(this, _currentDisplayInfo);
//        }

//        private void UpdateDisplayInfo()
//        {
//            var info = DeviceDisplay.Current.MainDisplayInfo;
//            _currentDisplayInfo.Width = info.Width / info.Density; // Konversi ke unit independen
//            _currentDisplayInfo.Height = info.Height / info.Density;
//            _currentDisplayInfo.Density = info.Density;
//            _currentDisplayInfo.Orientation = info.Orientation.ToString();
//            // Status IsScreenOn dikelola oleh ScreenSaver
//            _currentDisplayInfo.IsScreenOn = DeviceDisplay.Current.KeepScreenOn;
//        }
//        public void SetScreenLock(bool keepScreenOn)
//        {
//            DeviceDisplay.Current.KeepScreenOn = keepScreenOn;
//            UpdateDisplayInfo();
//            MainDisplayInfoChanged?.Invoke(this, _currentDisplayInfo);
//        }

//        public void Dispose()
//        {
//            // Penting: Hapus langganan saat service dibuang
//            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
//        }
//    }
//}
