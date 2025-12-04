using VideoCatalogue.Models;
using Xunit;

namespace VideoCatalogue.Tests
{
    public class VideoFileInfoTests
    {
        [Fact]
        public void FileSizeDisplay_FormatsKilobytes()
        {
            var info = new VideoFileInfo { FileSizeBytes = 2048 }; // 2KB
            Assert.Equal("2.0 KB", info.FileSizeDisplay);
        }

        [Fact]
        public void FileSizeDisplay_FormatsMegabytes()
        {
            var info = new VideoFileInfo { FileSizeBytes = 5 * 1024 * 1024 }; // 5MB
            Assert.Equal("5.0 MB", info.FileSizeDisplay);
        }
    }
}
