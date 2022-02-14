﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Holidays
{
    public class GetAllHolidaysQuery : IRequest<List<HolidayDto>>
    {
    }
}
