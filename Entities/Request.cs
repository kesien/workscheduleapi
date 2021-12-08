using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Entities
{
    public class Request : BaseEntity
    {
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
        public User User { get; set; }
    }
}
