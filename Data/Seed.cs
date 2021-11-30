using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var password = "password";
                var users = new List<User>
                {
                    new User
                    {
                        UserName = "admin",
                        Name = "admin",
                        Role = UserRole.ADMIN
                    },
                    new User
                    {
                        UserName = "testuser",
                        Name = "test",
                        Role = UserRole.USER
                    },
                    new User
                    {
                        UserName = "testuser2",
                        Name = "test",
                        Role = UserRole.USER
                    },
                    new User
                    {
                        UserName = "testuser3",
                        Name = "test",
                        Role = UserRole.USER
                    },
                    new User
                    {
                        UserName = "testuser4",
                        Name = "test",
                        Role = UserRole.USER
                    }
                };
                foreach (var user in users)
                {
                    userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                    if (user.UserName == "admin")
                    {
                        userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
                    }
                    else
                    {
                        userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
                    }
                }

            }
        }

        public static void SeedHolidays(ApplicationDbContext context)
        {
            if (!context.Holidays.Any()) 
            {
                var holidaysData = System.IO.File.ReadAllText("Data/holidays.json");
                var holidays = JsonConvert.DeserializeObject<List<Holiday>>(holidaysData);
                foreach (var holiday in holidays)
                {
                    holiday.IsFix = true;
                    context.Holidays.Add(holiday);
                }
                context.SaveChanges();
            }
        }
    }
}
