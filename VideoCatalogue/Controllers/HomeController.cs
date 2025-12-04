using Microsoft.AspNetCore.Mvc;
using VideoCatalogue.Models;

namespace VideoCatalogue.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index(string view = "catalogue")
        {
            var mediaPath = Path.Combine(_env.ContentRootPath, "media");
            if (!Directory.Exists(mediaPath))
            {
                Directory.CreateDirectory(mediaPath);
            }

            var files = Directory
                .EnumerateFiles(mediaPath, "*.mp4")
                .Select(path => new VideoFileInfo
                {
                    FileName = Path.GetFileName(path),
                    FileSizeBytes = new FileInfo(path).Length
                })
                .OrderBy(f => f.FileName)
                .ToList();

            ViewBag.CurrentView = view; // "upload" or "catalogue"
            return View(files);
        }
    }
}
