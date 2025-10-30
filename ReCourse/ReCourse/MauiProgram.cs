using Microsoft.Extensions.Logging;
using ReCourse.Services;
using ReCourse.Shared.Services;

namespace ReCourse
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the ReCourse.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

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
