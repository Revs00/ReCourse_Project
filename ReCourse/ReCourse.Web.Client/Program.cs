using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReCourse.Shared.Services;
using ReCourse.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the ReCourse.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
// Tambahkan baris ini
builder.Services.AddSingleton<ICameraService, ReCourse.Web.Client.Services.WebCameraService>();
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7010/"); // sesuaikan port backend
});

await builder.Build().RunAsync();
