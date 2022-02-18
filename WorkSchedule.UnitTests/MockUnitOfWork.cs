using Moq;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;

namespace WorkSchedule.UnitTests
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
