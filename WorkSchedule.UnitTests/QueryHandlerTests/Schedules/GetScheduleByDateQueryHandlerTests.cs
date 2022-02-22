using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.FileService;
using WorkSchedule.Application.Services.ScheduleService;
using WorkSchedule.UnitTests.Helpers;
using WorkSchedule.UnitTests.MockRepositories;
using WorkSchedule.UnitTests.MockServices;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Schedules
{
    public class GetScheduleByDateQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IScheduleService _scheduleService;
        private readonly IFileService _fileService;
        public GetScheduleByDateQueryHandlerTests()
        {
            var entitites = GenerateEntities();
            var holidayMockRepo = new MockGenericRepository<Holiday>(GetHolidays());
            _fileService = MockFileService.GetFileServiceMock(entitites[1].WordFile).Object;
            _mapper = MappingHelper.GetMappings().CreateMapper();
            var uowMock = MockUnitOfWork.GetUnitOfWorkMock();
            var repo = new MockScheduleRepository(entitites);
            uowMock.Setup(r => r.ScheduleRepository).Returns(repo.GetScheduleRepository().Object);
            _uow = uowMock.Object;
            _scheduleService = new ScheduleService(_uow, _fileService);
        }

        private List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User { Id = Guid.Parse("b0844c05-e80b-442e-0000-25470ee6c970"), Name = "test", UserName = "test" },
                new User { Id = Guid.Parse("b0844c05-e80b-442e-1111-25470ee6c970"), Name = "test2", UserName = "test2" }
            };
        }

        private List<Request> GetRequests()
        {
            return new List<Request>
            {
                new Request { Id = Guid.Empty, Date = DateTime.Parse("2022-02-01"), Type = Api.Constants.RequestType.MORNING, User = GenerateUsers()[0] }
            };
        }

        private List<Holiday> GetHolidays()
        {
            return new List<Holiday>();
        }

        private List<MonthlySchedule> GenerateEntities()
        {
            var user = new User { Id = Guid.Parse("b0844c05-e80b-442e-0000-25470ee6c970"), Name = "test", UserName = "test" };
            var user2 = new User { Id = Guid.Parse("b0844c05-e80b-442e-1111-25470ee6c970"), Name = "test2", UserName = "test2" };
            var entities = new List<MonthlySchedule>
            {
                new MonthlySchedule
                {
                    Id = Guid.Parse("b0844c05-aaaa-442e-0000-25470ee6c970"),
                    IsSaved = false,
                    NumOfWorkdays = 1,
                    Year = 2022,
                    Month = 2,
                    WordFile = null,
                    Days = new List<Day>
                    {
                        new Day
                        {
                            Id = Guid.Empty,
                            Date = DateTime.Parse("2022-02-01"),
                            IsHoliday = false,
                            IsWeekend = false,
                            UsersOnHoliday = new List<HolidaySchedule>(),
                            UsersScheduledForForenoon = new List<Forenoonschedule>
                            {
                                new Forenoonschedule
                                {
                                    Id = Guid.Empty,
                                    IsRequest = false,
                                    User = user
                                }
                            },
                            UsersScheduledForMorning = new List<MorningSchedule>
                            {
                                new MorningSchedule
                                {
                                    Id = Guid.Empty,
                                    IsRequest = false,
                                    User = user2
                                }
                            }

                        }
                    }
                },
                new MonthlySchedule
                {
                    Id = Guid.Parse("b0844c05-bbbb-442e-0000-25470ee6c970"),
                    IsSaved = true,
                    NumOfWorkdays = 1,
                    Year = 2022,
                    Month = 3,
                    Days = new List<Day>
                    {
                        new Day
                        {
                            Id = Guid.Empty,
                            Date = DateTime.Parse("2022-03-01"),
                            IsHoliday = false,
                            IsWeekend = false,
                            UsersOnHoliday = new List<HolidaySchedule>(),
                            UsersScheduledForForenoon = new List<Forenoonschedule>
                            {
                                new Forenoonschedule
                                {
                                    Id = Guid.Empty,
                                    IsRequest = false,
                                    User = user2
                                }
                            },
                            UsersScheduledForMorning = new List<MorningSchedule>
                            {
                                new MorningSchedule
                                {
                                    Id = Guid.Empty,
                                    IsRequest = false,
                                    User = user
                                }
                            }

                        }
                    }
                }
            };
            var WordFile = new WordFile
            {
                Id = Guid.Parse("0000000-e80b-442e-0000-25470ee6c970"),
                FileName = "test.docx",
                FilePath = "./test.docx",
                MonthlyScheduleId = Guid.Parse("b0844c05-bbbb-442e-0000-25470ee6c970")
            };
            entities[1].WordFile = WordFile;
            return entities;
        }
    }
}
