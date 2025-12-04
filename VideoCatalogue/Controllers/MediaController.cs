using Microsoft.AspNetCore.Mvc;
using VideoCatalogue.Models;

namespace VideoCatalogue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly UploadValidator _validator = new();

        public MediaController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GetMediaPath()
        {
            var mediaPath = Path.Combine(_env.ContentRootPath, "media");
            if (!Directory.Exists(mediaPath))
            {
                Directory.CreateDirectory(mediaPath);
            }
            return mediaPath;
        }

        [HttpPost("upload")]
        [RequestSizeLimit(UploadValidator.MaxUploadBytes)]
        [RequestFormLimits(MultipartBodyLengthLimit = UploadValidator.MaxUploadBytes)]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            var mediaPath = GetMediaPath();
            long totalSize = files.Sum(f => f.Length);
            if (!_validator.IsWithinSizeLimit(totalSize))
            {
                return StatusCode(StatusCodes.Status413PayloadTooLarge,
                    "Total upload size exceeds 200MB.");
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                if (!_validator.IsValidExtension(file.FileName))
                {
                    return BadRequest("Only .mp4 files are allowed.");
                }

                var safeFileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(mediaPath, safeFileName);

                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { success = true });
        }

        // DELETE /api/media/delete?fileName=xxx.mp4
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("File name is required.");

            var mediaPath = GetMediaPath();
            var safeFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(mediaPath, safeFileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            System.IO.File.Delete(filePath);
            return Ok(new { success = true });
        }

        // DELETE /api/media/deleteAll
        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {
            var mediaPath = GetMediaPath();
            var dir = new DirectoryInfo(mediaPath);
            foreach (var file in dir.GetFiles("*.mp4"))
            {
                file.Delete();
            }

            return Ok(new { success = true });
        }
    }
}
