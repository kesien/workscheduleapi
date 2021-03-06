using Moq;
using WorkSchedule.Application.Data;

namespace WorkSchedule.UnitTests
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(r => r.Save()).Verifiable();
            return mock;
        }
    }
}
