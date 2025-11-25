using Microsoft.Extensions.Logging;
using ReCourse.Maui.Services;
using ReCourse.Maui.Shared.Services;
using ReCourse.Shared.Services;

namespace ReCourse.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(/* ... */)
                .Services
                .AddMauiBlazorWebView();

            // register HttpClient dengan base address ke backend (sesuaikan port)
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

            // Jika ApiService ada di Shared yang membutuhkan HttpClient ctor:
            builder.Services.AddScoped<ApiService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            return builder.Build();
        }
    }
}