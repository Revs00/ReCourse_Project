using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ReCourse.Backend.Data;
using ReCourse.Backend.Models;
using System.IO; // WAJIB: Tambahkan ini untuk Path.Combine

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Using SQLite Database
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("TrainingDb"));

// Allow MAUI and browsers in dev
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// ----- BAGIAN KONFIGURASI PIPELINE (app.Use...) -----

// 1. Pengembangan/Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Keamanan & Pengalihan
app.UseHttpsRedirection();
app.UseCors(); // Cors harus di awal

// 3. Konfigurasi File Statis (WAJIB DITEMPATKAN SEBELUM MapControllers)
// Penggunaan app.Environment.ContentRootPath adalah path yang stabil
var uploadsDirectory = Path.Combine(app.Environment.ContentRootPath, "Uploads");

// Pastikan direktori ada sebelum digunakan (mencegah System.IO.DirectoryNotFoundException)
if (!Directory.Exists(uploadsDirectory))
{
    Directory.CreateDirectory(uploadsDirectory);
    Console.WriteLine($"[INFO] Folder Uploads dibuat di: {uploadsDirectory}");
}

// Konfigurasi File Statis Standar (untuk wwwroot)
app.UseStaticFiles();

// Konfigurasi File Statis Khusus untuk Folder Uploads
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDirectory),
    // Path URL yang digunakan browser: https://localhost:7010/uploads/namafile.jpg
    RequestPath = "/uploads" // Catatan: Gunakan huruf kecil, ini sudah sesuai dengan standar URL
});


// 4. Autorisasi dan Routing
app.UseAuthorization();
app.MapControllers();


// Seed sample data (only when DB empty)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Trainers.Any())
    {
        var t1 = new Trainer { FullName = "Budi Sutedjo", Email = "budis@gmail.com", Expertise = "Data Management", ExperienceYear = 20, PhoneNumber = "081000001", Bio = "Don't say busy when you're not as busy as myself!" };
        var t2 = new Trainer { FullName = "Mario Sutopo", Email = "marios@gmail.com", Expertise = "Data Science", ExperienceYear = 5, PhoneNumber = "082000002", Bio = "Data enthusiast" };
        var t3 = new Trainer { FullName = "Daniko Kevin", Email = "danikok@gmail.com", Expertise = "Machine Learning", ExperienceYear = 10, PhoneNumber = "083000003", Bio = "Machine Learning enthusiast" };
        db.Trainers.AddRange(t1, t2, t3);
        db.Courses.AddRange(
            new Course { Title = "Dasar-Dasar Manajemen Organisasi", Description = "Belajar DDMO bersama Bapak Budi", DurationMinutes = 120, Price = 120, Level = "Intermediate", Trainer = t1 },
            new Course { Title = "Data Mining", Description = "Belajar Data Mining bersama Koko Mario", DurationMinutes = 120, Price = 120, Level = "Intermediate", Trainer = t2 },
            new Course { Title = "Machine Learning", Description = "Basics of Machine Learning", DurationMinutes = 75, Price = 90, Level = "Beginner", Trainer = t3 },
            new Course { Title = "Logaritma Pemrograman", Description = "Belajar Logaritma Pemrograman bersama Bapak Daniko", DurationMinutes = 60, Price = 150, Level = "Beginner", Trainer = t3 },
            new Course { Title = "Data Warehouse", Description = "Belajar Data Warehouse bersama Koko Mario", DurationMinutes = 150, Price = 180, Level = "Intermediate", Trainer = t2 }
        );
        db.SaveChanges();
    }
}

app.Run();