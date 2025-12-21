using Microsoft.Extensions.Logging;
using ReCourse.Services;
using ReCourse.Shared.Services;
using CommunityToolkit.Maui;

namespace ReCourse
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // WAJIB: Tambahkan baris ini
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ReCourse.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            // Tambahkan baris ini
            builder.Services.AddSingleton<ICameraService, ReCourse.Services.MauiCameraService>();
            // TAMBAHKAN REGISTRASI NETWORK SERVICE (MAUI)
            builder.Services.AddSingleton<INetworkService, MauiNetworkService>();
            // TAMBAHKAN REGISTRASI GEOLOCATION SERVICE (MAUI)
            builder.Services.AddSingleton<IGeolocationService, MauiGeolocationService>();
            // TAMBAHKAN REGISTRASI BATTERY SERVICE (MAUI)
            builder.Services.AddSingleton<IBatteryService, MauiBatteryService>();
            // TAMBAHKAN REGISTRASI GEOCODING SERVICE (MAUI)
            builder.Services.AddSingleton<IGeocodingService, MauiGeocodingService>();
            // TAMBAHKAN REGISTRASI SCREENSHOT SERVICE (MAUI)
            builder.Services.AddSingleton<IScreenshotService, MauiScreenshotService>();
            // TAMBAHKAN REGISTRASI SPEECH SERVICE (MAUI)
            builder.Services.AddSingleton<ISpeechService, MauiSpeechService>();
            // TAMBAHKAN REGISTRASI LOGIN SERVICE (MAUI)
            builder.Services.AddSingleton<AuthService>();
            // TAMBAHKAN REGISTRASI DEVICE DISPLAY SERVICE (MAUI)
            //builder.Services.AddSingleton<IDeviceDisplayService, MauiDeviceDisplayService>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                // sesuaikan dengan port backend kamu
                client.BaseAddress = new Uri("https://localhost:7010/");
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
