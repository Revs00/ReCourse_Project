using Microsoft.AspNetCore.Components.Forms; // <--- tambahkan ini
using ReCourse.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
            // Menggunakan ukuran file yang sama dengan batas di Controller Backend
            var maxAllowedSize = 5 * 1024 * 1024;

            // Pengecekan ukuran file (opsional, tetapi disarankan)
            if (file.Size > maxAllowedSize)
            {
                // Lempar exception yang bisa ditangani di Blazor
                throw new InvalidOperationException($"Ukuran file terlalu besar. Maksimal {maxAllowedSize / 1024 / 1024}MB.");
            }

            using var content = new MultipartFormDataContent();
            // Gunakan StreamContent untuk membaca stream file
            // Hati-hati: Nama form field "file" HARUS sama dengan parameter IFormFile file di Controller Backend
            content.Add(new StreamContent(file.OpenReadStream(maxAllowedSize)),
                        name: "file",
                        fileName: file.Name);

            // Panggil endpoint baru: POST /api/Upload/File
            var response = await _http.PostAsync("api/Upload/File", content);

            // Cek status code
            if (response.IsSuccessStatusCode)
            {
                // Controller mengembalikan nama file yang disimpan (string)
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Upload API Gagal: {error}");
                // Lempar exception agar error diketahui di Frontend
                throw new HttpRequestException($"Gagal mengunggah file: {response.ReasonPhrase}. Detail: {error}");
            }
        }

        // Modifikasi metode lama agar menggunakan metode baru
        public async Task<string?> UploadCourseImageAsync(IBrowserFile file, long maxFileSize = 1024 * 1024 * 5)
        {
            using var stream = file.OpenReadStream(maxFileSize);
            return await UploadStreamAsync(stream, file.Name);
        }


        // ⬇️ FILE UPLOAD BARU DENGAN BASE64 DARI KAMERA ⬇️
        public async Task<string> UploadBase64Image(string base64Data, string extension)
        {
            var payload = new { Base64Image = base64Data, Extension = extension };

            // Panggil endpoint baru: POST /api/Upload/Base64
            var response = await _http.PostAsJsonAsync("api/Upload/Base64", payload);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Upload Base64 API Gagal: {error}");
                throw new HttpRequestException($"Gagal mengunggah gambar Base64: {response.ReasonPhrase}. Detail: {error}");
            }
        }

        // Tambahkan metode ini
        public async Task<string?> UploadStreamAsync(Stream stream, string fileName)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                using var streamContent = new StreamContent(stream);
                content.Add(streamContent, "file", fileName);

                var response = await _http.PostAsync("api/Upload/File", content);
                if (!response.IsSuccessStatusCode) return null;

                //var jsonResponse = await response.Content.ReadAsStringAsync();
                //var result = JsonSerializer.Deserialize<UploadResult>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //return result?.FilePath;
                // Ganti menjadi:
                var returnedFileName = await response.Content.ReadAsStringAsync();
                // Karena Controller mengembalikan string "a1b2c3d4.jpg", kita kembalikan saja string itu.
                return returnedFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image upload failed: {ex.Message}");
                return null;
            }
        }
    }
}
