using Moq;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency;

namespace WorkSchedule.UnitTests
{
    public class MockUnitOfWork
    {
        public MockUnitOfWork()
        {

        }

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            var dbContextMock = new Mock<ApplicationDbContext>();
            var mock = new Mock<IUnitOfWork>();
            return mock;
        }
    }
}
