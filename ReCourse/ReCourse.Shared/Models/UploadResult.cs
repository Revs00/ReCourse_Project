using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Models
{
    public class UploadResult
    {
        // Sesuaikan nama properti ini (misalnya FilePath) agar sesuai dengan JSON 
        // yang dikembalikan oleh Controller API /api/Upload/File Anda.
        public string? FilePath { get; set; }
        // public string? ErrorMessage { get; set; } // Opsional: untuk menangani error
    }
}
