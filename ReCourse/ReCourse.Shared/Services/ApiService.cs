using ReCourse.Shared.Models;
using Microsoft.AspNetCore.Components.Forms; // <--- tambahkan ini
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        public ApiService(HttpClient http) => _http = http;

        // Trainers
        public Task<List<Trainer>> GetTrainers(string? search = null)
        {
            var url = "api/trainers" + (string.IsNullOrWhiteSpace(search) ? "" : $"?search={Uri.EscapeDataString(search)}");
            return _http.GetFromJsonAsync<List<Trainer>>(url) ?? Task.FromResult(new List<Trainer>());
        }
        public Task<Trainer?> GetTrainer(int id) => _http.GetFromJsonAsync<Trainer>($"api/trainers/{id}");
        public Task<HttpResponseMessage> CreateTrainer(Trainer t) => _http.PostAsJsonAsync("api/trainers", t);
        public Task<HttpResponseMessage> UpdateTrainer(int id, Trainer t) => _http.PutAsJsonAsync($"api/trainers/{id}", t);
        public Task<HttpResponseMessage> DeleteTrainer(int id) => _http.DeleteAsync($"api/trainers/{id}");

        // Courses
        public Task<List<Course>> GetCourses(string? search = null)
        {
            var url = "api/courses" + (string.IsNullOrWhiteSpace(search) ? "" : $"?search={Uri.EscapeDataString(search)}");
            return _http.GetFromJsonAsync<List<Course>>(url) ?? Task.FromResult(new List<Course>());
        }
        public Task<Course?> GetCourse(int id) => _http.GetFromJsonAsync<Course>($"api/courses/{id}");
        public Task<HttpResponseMessage> CreateCourse(Course c) => _http.PostAsJsonAsync("api/courses", c);
        public Task<HttpResponseMessage> UpdateCourse(int id, Course c) => _http.PutAsJsonAsync($"api/courses/{id}", c);
        public Task<HttpResponseMessage> DeleteCourse(int id) => _http.DeleteAsync($"api/courses/{id}");

        // File Upload
        public async Task<string> UploadFile(IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream(5 * 1024 * 1024);

            content.Add(new StreamContent(stream), "file", file.Name);

            var response = await _http.PostAsync("api/Upload/UploadFile", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
