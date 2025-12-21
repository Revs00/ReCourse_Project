using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public interface IScreenshotService
    {
        Task<byte[]> CaptureScreenshotAsync();
    }
}
