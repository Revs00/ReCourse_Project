using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ReCourse.Backend.Data;
using ReCourse.Backend.Models;

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
/*builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));)*/

// Allow MAUI and browsers in dev
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles(); // penting!
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});

app.MapControllers();

app.UseHttpsRedirection();

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
