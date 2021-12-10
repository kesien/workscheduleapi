namespace WorkScheduleMaker.Entities
{
    public class MonthlySchedule : BaseEntity
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public ICollection<Day> Days { get; set; }
        public ICollection<Summary> Summaries { get; set; }
        public int NumOfWorkdays { get; set; }
        public bool IsSaved { get; set; }
        public WordFile? WordFile { get; set; }
    }
}
