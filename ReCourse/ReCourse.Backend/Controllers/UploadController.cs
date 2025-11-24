using ReCourse.Backend.PublicClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReCourse.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : Controller
    {
        [HttpPost] //api/main/uploadfile
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UploadFile(IFormFile file)
        {
            return Ok(new UploadHandler().Upload(file));
        }
    }
}
