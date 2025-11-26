using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class CameraResult
    {
        public Stream? Stream { get; set; }
        public string FileName { get; set; } = "";
    }
    public interface ICameraService
    {
        Task<CameraResult?> TakePhotoAsync();
    }
}
