using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting; // Untuk mendapatkan path root konten

namespace ReCourse.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        // Tentukan ukuran maksimal file yang diizinkan (misalnya 5MB)
        private const long MaxFileSize = 5 * 1024 * 1024;

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // Endpoint yang dipanggil dari ApiService: POST /api/Upload/File
        [HttpPost("File")]
        // Menggunakan IFormFile untuk menerima file dari request multipart/form-data
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File tidak ditemukan.");
            }

            if (file.Length > MaxFileSize)
            {
                return BadRequest($"Ukuran file melebihi batas {MaxFileSize / 1024 / 1024}MB.");
            }

            try
            {
                // 1. Tentukan folder Uploads (Pastikan path stabil menggunakan ContentRootPath)
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads");

                // Pastikan direktori ada (ini mencegah System.IO.DirectoryNotFoundException)
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 2. Buat nama file unik untuk menghindari konflik (misalnya: GUID-NamaAsli.jpg)
                var extension = Path.GetExtension(file.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // 3. Simpan file ke disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 4. Kirim kembali NAMA FILE yang baru dibuat
                return Ok(uniqueFileName);
            }
            catch (Exception ex)
            {
                // Catat error ke console untuk debugging
                Console.WriteLine($"Error saat mengunggah file: {ex.Message}");
                return StatusCode(500, "Gagal saat memproses file di server.");
            }
        }
    }
}