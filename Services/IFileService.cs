using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IFileService
    {
        Task<WordFile> GenerateWordDoc(MonthlySchedule schedule, int max);
        Task DeleteFile(string path);
    }
}