using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;

namespace WorkSchedule.Application.Services.DropboxService
{
    public class DropboxService : IDropboxService
    {
        private readonly string _accessToken;
        private readonly string _uploadDir;

        public DropboxService(IConfiguration config)
        {
            _accessToken = config.GetSection("DROPBOX:TOKEN").Value;
            _uploadDir = config.GetSection("DROPBOX:DIRECTORY").Value;
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            using (var _dropBox = new DropboxClient(_accessToken))
            {
                try
                {
                    await _dropBox.Files.DeleteV2Async($"{_uploadDir}/{fileName}");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return false;
            }
        }

        public async Task<byte[]> GetFile(string fileName)
        {
            using (var _dropBox = new DropboxClient(_accessToken))
            {
                using (var _response = await _dropBox.Files.DownloadAsync($"{_uploadDir}/{fileName}"))
                {
                    return await _response.GetContentAsByteArrayAsync();
                }
            }
        }

        public async Task<bool> UploadFile(string fileName, MemoryStream memoryStream)
        {
            using (var _dropBox = new DropboxClient(_accessToken))
            {
                try
                {
                    await _dropBox.Files.UploadAsync($"{_uploadDir}/{fileName}", WriteMode.Overwrite.Instance, body: memoryStream);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return false;
            }
        }
    }
}