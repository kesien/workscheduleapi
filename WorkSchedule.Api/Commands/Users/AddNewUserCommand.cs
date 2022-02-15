using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Users
{
    public class AddNewUserCommand : IRequest<UserToListDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
