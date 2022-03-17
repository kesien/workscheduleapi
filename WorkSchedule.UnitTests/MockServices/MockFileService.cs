using Moq;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.FileService;

namespace WorkSchedule.UnitTests.MockServices
{
    public static class MockFileService
    {
        public static WordFile WordFile { get; set; }
        public static Mock<IFileService> GetFileServiceMock(WordFile wordFile)
        {
            WordFile = wordFile;
            var mock = new Mock<IFileService>();
            mock.Setup(r => r.GenerateWordDoc(It.IsAny<MonthlySchedule>(), It.IsAny<int>()))
                .ReturnsAsync(wordFile);
            return mock;
        }
    }
}
