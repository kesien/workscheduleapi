using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Requests
{
    public class AddNewRequestCommand : IRequest<RequestDto>
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
    }
}
