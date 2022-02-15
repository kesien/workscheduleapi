using Microsoft.AspNetCore.Identity;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data.Seeds
{
    public static class UserSeed
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
                        UserName = "balazsnobik@gmail.com",
                        Name = "Admin",
                        Role = UserRole.ADMIN
                    }
                };
                foreach (var user in users)
                {
                    userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                    if (user.Role == UserRole.ADMIN)
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

        
    }
}
