using Newtonsoft.Json;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data.Seeds
{
    public static class HolidaySeed
    {
        public static void SeedHolidays(ApplicationDbContext context)
        {
            if (!context.Holidays.Any())
            {
                var holidaysData = File.ReadAllText("../WorkSchedule.Application/Resources/holidays.json");
                var holidays = JsonConvert.DeserializeObject<List<Holiday>>(holidaysData);
                if (holidays != null)
                {
                    foreach (var holiday in holidays)
                    {
                        holiday.IsFix = true;
                        context.Holidays.Add(holiday);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
