using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduleMaker.Services
{
    public interface IDropboxService
    {
        Task<byte[]> GetFile(string file);
        Task<bool> UploadFile(string file, MemoryStream fileStream);
        Task<bool> DeleteFile(string file);
    }
}