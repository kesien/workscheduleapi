using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
