//using System;
//using ReCourse.Shared.Services;

//namespace ReCourse.Web.Client.Services
//{
//    // Placeholder untuk layanan tampilan perangkat di platform web
//    // Menggunakan nilai default. Lock Screen tidak bisa dikontrol dari Blazor WASM.
//    public class WebDeviceDisplayService : IDeviceDisplayService
//    {
//        public event EventHandler<DisplayData> MainDisplayInfoChanged;
//        public DisplayData CurrentDisplayInfo => new DisplayData
//        {
//            Width = 1200,
//            Height = 800,
//            Density = 1.0,
//            Orientation = "Landscape",
//            IsScreenOn = true // Asumsi layar selalu menyala di web
//        };

//        public void SetScreenLock(bool keepScreenOn)
//        {
//            // Tidak melakukan apa-apa di web
//            // Di Blazor Server/WASM, fitur ini harus diimplementasikan melalui JSInterop jika diperlukan
//        }
//    }
//}
