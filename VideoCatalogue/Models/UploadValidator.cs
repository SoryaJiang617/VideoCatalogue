using System;

namespace VideoCatalogue.Models
{
    public class UploadValidator
    {
        public const long MaxUploadBytes = 200L * 1024L * 1024L; // 200MB

        public bool IsValidExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var ext = Path.GetExtension(fileName);
            return ext.Equals(".mp4", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsWithinSizeLimit(long totalBytes) =>
            totalBytes <= MaxUploadBytes;
    }
}
