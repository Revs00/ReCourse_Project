using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReCourse.Shared.Services;

namespace ReCourse.Services
{
    public class MauiScreenshotService : IScreenshotService
    {
        public async Task<byte[]> CaptureScreenshotAsync()
        {
            if (Screenshot.Default.IsCaptureSupported)
            {
                // Berikan delay kecil (misal 200-500ms) agar WebView sempat merender buffer ke sistem
                await Task.Delay(300);
                // Menangkap layar saat ini
                IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

                // Konversi ke stream
                using var stream = await screen.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
            return null;
        }
    }
}
