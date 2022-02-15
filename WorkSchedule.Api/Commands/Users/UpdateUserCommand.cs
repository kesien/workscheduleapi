using MediatR;
using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Commands.Users
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string RequesterId { get; }
        public string Id { get; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public UserRole? Role { get; set; }

        public UpdateUserCommand(string requesterId, string id)
        {
            RequesterId = requesterId;
            Id = id;
        }
    }
}
