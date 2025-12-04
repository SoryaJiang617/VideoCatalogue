using VideoCatalogue.Models;
using Xunit;

namespace VideoCatalogue.Tests
{
    public class UploadValidatorTests
    {
        [Fact]
        public void IsValidExtension_AllowsMp4()
        {
            var v = new UploadValidator();
            Assert.True(v.IsValidExtension("video.MP4"));
        }

        [Fact]
        public void IsValidExtension_RejectsNonMp4()
        {
            var v = new UploadValidator();
            Assert.False(v.IsValidExtension("video.avi"));
        }

        [Theory]
        [InlineData(100 * 1024 * 1024, true)]
        [InlineData(200 * 1024 * 1024, true)]
        [InlineData(201 * 1024 * 1024, false)]
        public void IsWithinSizeLimit_ChecksTotalSize(long bytes, bool expected)
        {
            var v = new UploadValidator();
            Assert.Equal(expected, v.IsWithinSizeLimit(bytes));
        }
    }
}
