using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class UserToRequestDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsRequest { get; set; }
    }
}