using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
        var t1 = new Trainer { FullName = "Budi Sutedjo", Email = "budis@gmail.com", Expertise = "Data Management", ExperienceYear = 6, PhoneNumber = "081000001", Bio = "Don't say busy when you're not as busy as myself!" };
        var t2 = new Trainer { FullName = "Mario Sutopo", Email = "marios@gmail.com", Expertise = "Data Science", ExperienceYear = 4, PhoneNumber = "082000002", Bio = "Data enthusiast" };
        db.Trainers.AddRange(t1, t2);
        db.Courses.AddRange(
            new Course { Title = "Dasar-Dasar Manajemen Organisasi", Description = "Belajar DDMO bersama Bapak Budi", DurationMinutes = 120, Price = 120, Level = "Intermediate", Trainer = t1 },
            new Course { Title = "Introduction to Data Science", Description = "Basics of data processing and Machine Learning", DurationMinutes = 75, Price = 90, Level = "Beginner", Trainer = t2 }
        );
        db.SaveChanges();
    }
}

app.Run();
