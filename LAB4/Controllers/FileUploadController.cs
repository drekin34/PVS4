using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace LAB4.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ViewBag.Message = "Файл завантажено успішно!";
            }
            else
            {
                ViewBag.Message = "Будь ласка, виберіть файл для завантаження.";
            }

            return View("Index");
        }

        public IActionResult ViewFiles()
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files"));
            return View(files);
        }

        public IActionResult ViewFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);
            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", fileName);
            }
            return NotFound();
        }
    }
}
