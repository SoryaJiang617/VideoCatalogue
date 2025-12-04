namespace VideoCatalogue.Models
{
    public class VideoFileInfo
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }

        public string FileSizeDisplay =>
            FileSizeBytes > 1024 * 1024
                ? $"{FileSizeBytes / 1024.0 / 1024.0:F1} MB"
                : $"{FileSizeBytes / 1024.0:F1} KB";
    }
}

