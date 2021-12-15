using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace WorkScheduleMaker.Services
{
    public class DropboxService : IDropboxService
    {
        private readonly string _accessToken;

        public DropboxService(IConfiguration config)
        {
            _accessToken = config.GetSection("DropboxToken").Value;
        }

        public async Task<bool> DeleteFile(string file)
        {
            using (var _dropBox = new DropboxClient(_accessToken))
            {
                try
                {
                    await _dropBox.Files.DeleteAsync(file);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return false;
            }
        }

        public async Task<byte[]> GetFile(string file)
        {
            using (var _dropBox = new DropboxClient(_accessToken)) 
            {
                using (var _response = await _dropBox.Files.DownloadAsync(file))
                {
                    return await _response.GetContentAsByteArrayAsync();
                }
            }
        }

        public async Task<bool> UploadFile(string file, MemoryStream fileStream)
        {
            var fileInfo = new FileInfo(file);
            var fileName = fileInfo.Name;
            var dirName = fileInfo.Directory.Name;
            using (var _dropBox = new DropboxClient(_accessToken))
            {
                try
                {
                    await _dropBox.Files.UploadAsync($"/{dirName}/{fileName}", WriteMode.Overwrite.Instance, body: fileStream);
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