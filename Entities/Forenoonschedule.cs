namespace WorkScheduleMaker.Entities
{
    public class Forenoonschedule
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsRequest { get; set; }
    }
}
