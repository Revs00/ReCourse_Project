using ReCourse.Shared.Services;
using ReCourse.Web.Components;
using ReCourse.Web.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the ReCourse.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7010/"); // sesuaikan port backend
});

var app = builder.Build();

var uploadsDirectory = Path.Combine(app.Environment.ContentRootPath, "Uploads");
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(uploadsDirectory),
//    // Tentukan URL path yang akan dilayani oleh server API:
//    RequestPath = "/uploads"
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(ReCourse.Shared.Models.Course).Assembly,
        typeof(ReCourse.Web.Client._Imports).Assembly);

app.Run();
