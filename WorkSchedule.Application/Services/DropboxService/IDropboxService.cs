namespace WorkSchedule.Application.Services.DropboxService
{
    public interface IDropboxService
    {
        Task<byte[]> GetFile(string fileName);
        Task<bool> UploadFile(string fileName, MemoryStream memoryStream);
        Task<bool> DeleteFile(string fileName);
    }
}