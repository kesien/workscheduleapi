using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Dtos
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
    }
}
