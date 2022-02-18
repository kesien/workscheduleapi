﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Users
{
    public class GetUserByIdQuery : IRequest<UserToListDto>
    {
        public string RequesterId { get; set; }
        public string Id { get; set; }
    }
}