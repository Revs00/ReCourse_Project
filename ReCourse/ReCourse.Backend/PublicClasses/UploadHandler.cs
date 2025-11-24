using System.Globalization;

namespace ReCourse.Backend.PublicClasses
{
    public class UploadHandler
    {
        public string Upload(IFormFile file)
        {
            // Extention
            List<string> validExtentions = new List<string>() { ".jpg", ".png", ".pdf", ".docx", ".jpeg" };
            string extention = Path.GetExtension(file.FileName);
            if (!validExtentions.Contains(extention))
            {
                return $"Extention is not valid ({string.Join(',', validExtentions)})";
            }

            // File Size
            long size = file.Length;
            if (size > 5 * 1024 * 1024)
            {
                return "Maximum size can be 5MB.";
            }

            // Name Changing
            string fileName = Guid.NewGuid().ToString() + extention;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }
    }
}
