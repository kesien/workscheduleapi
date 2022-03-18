using Newtonsoft.Json;
using System.Reflection;
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
                var resourceName = "WorkSchedule.Application.Resources.holidays.json";
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream data = assembly.GetManifestResourceStream(resourceName);
                var holidaysData = "";
                using (StreamReader reader = new StreamReader(data))
                {
                    holidaysData = reader.ReadToEnd();
                }
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
