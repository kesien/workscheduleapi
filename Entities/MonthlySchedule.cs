namespace WorkScheduleMaker.Entities
{
    public class MonthlySchedule
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public ICollection<Day> Days { get; set; }
        public ICollection<Summary> Summaries { get; set; }
        public int NumOfWorkdays { get; set; }
    }
}
