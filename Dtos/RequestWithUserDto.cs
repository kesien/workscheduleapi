using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class RequestWithUserDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}