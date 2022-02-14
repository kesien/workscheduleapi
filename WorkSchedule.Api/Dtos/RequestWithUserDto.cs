using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Dtos
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