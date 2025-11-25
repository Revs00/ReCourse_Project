using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Maui.Media;
//using ReCourse.Shared.Services;

namespace ReCourse.MauiHybrid.Services
{

    public interface ICameraService
    {
        Task<string?> CapturePhotoAsync();
    }

    //public class MauiCameraService : ICameraService
    //{
    //public async Task<string?> CapturePhotoAsync()
    //{
    //    // Permission
    //    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    //    if (status != PermissionStatus.Granted)
    //    {
    //        status = await Permissions.RequestAsync<Permissions.Camera>();
    //    }

    //    if (status != PermissionStatus.Granted)
    //        return null;

    //    // Take photo
    //    var result = await MediaPicker.Default.CapturePhotoAsync();
    //    if (result == null)
    //        return null;

    //    using var stream = await result.OpenReadAsync();
    //    using var ms = new MemoryStream();
    //    await stream.CopyToAsync(ms);

    //    return $"data:image/jpeg;base64,{Convert.ToBase64String(ms.ToArray())}";
    //}
    //}
}
