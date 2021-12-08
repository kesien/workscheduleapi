namespace WorkScheduleMaker.Entities
{
    public class Forenoonschedule : BaseEntity
    {
        public User User { get; set; }
        public bool IsRequest { get; set; }
        public Day Day { get; set; }
        
        
    }
}
