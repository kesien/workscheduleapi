using AutoMapper;
using WorkSchedule.Application.Helpers;

namespace WorkSchedule.UnitTests.Helpers
{
    public static class MappingHelper
    {
        public static MapperConfiguration GetMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
        }
    }
}
