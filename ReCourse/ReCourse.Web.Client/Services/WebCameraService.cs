using ReCourse.Shared.Services;

namespace ReCourse.Web.Client.Services
{
    public class WebCameraService : ICameraService
    {
        public Task<CameraResult?> TakePhotoAsync()
        {
            // Di web, user pakai <InputFile>, jadi ini return null saja
            return Task.FromResult<CameraResult?>(null);
        }
    }
}
