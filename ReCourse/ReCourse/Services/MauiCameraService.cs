using ReCourse.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace ReCourse.Services
{
    public class MauiCameraService : ICameraService
    {
        public async Task<CameraResult?> TakePhotoAsync()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        var stream = await photo.OpenReadAsync();
                        return new CameraResult
                        {
                            Stream = stream,
                            FileName = photo.FileName
                        };
                    }
                }
            }
            catch (Exception ex) 
            {
                // Handle error or cancel
                Console.WriteLine($"Camera Error: {ex.Message}");
            }
            return null;
        }
    }
}
