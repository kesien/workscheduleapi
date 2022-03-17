using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Dtos
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
    }
}
