using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace VideoCatalogue.Tests
{
    public class UploadIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UploadIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Home_Index_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UploadEndpoint_AllowsMp4File()
        {
            var content = new MultipartFormDataContent();

            var fakeBytes = new byte[100];
            var fileContent = new ByteArrayContent(fakeBytes);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");

            content.Add(fileContent, "files", "test.mp4");

            var response = await _client.PostAsync("/api/media/upload", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
