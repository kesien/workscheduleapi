
using WorkSchedule.Api.Constants;

namespace WorkSchedule.Application.Persistency.Entities
{
    public class Request : BaseEntity
    {
        public DateTime Date { get; set; }
        public RequestType Type { get; set; }
        public User User { get; set; }
    }
}
