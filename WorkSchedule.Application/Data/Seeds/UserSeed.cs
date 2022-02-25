using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSchedule.Application.Constants;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data.Seeds
{
    public static class UserSeed
    {
        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Id = Guid.Parse("ffc59f07-0034-4f83-b673-f21da9179c9d"),
                        Name = "User",
                        NormalizedName = "USER"
                    },
                    new Role
                    {
                        Id = Guid.Parse("1da58f4d-44e9-4460-b4b9-3877481affb1"),
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    }
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }
        }
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
