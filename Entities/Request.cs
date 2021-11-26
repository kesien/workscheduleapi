using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Entities
{
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
