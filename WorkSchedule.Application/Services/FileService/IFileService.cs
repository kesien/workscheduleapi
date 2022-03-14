using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.FileService
{
    public interface IFileService
    {
        Task<WordFile> GenerateWordDoc(MonthlySchedule schedule, int max);
    }
}