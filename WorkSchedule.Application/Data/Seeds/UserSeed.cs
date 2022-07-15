using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WorkSchedule.Application.Constants;
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
                    },
                    new Role
                    {
                        Id = Guid.Parse("3a1a2c1e-d7ee-4cbc-b054-9610f6d851a2"),
                        Name = "Superadmin",
                        NormalizedName = "SUPERADMIN"
                    }
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }
        }
        public static void SeedUsers(UserManager<User> userManager, IConfiguration config)
        {
            if (!userManager.Users.Any())
            {
                string adminPassword = config["AdminSettings:Password"];
                string adminUserName = config["AdminSettings:Username"];
                string adminName = config["AdminSettings:Name"];
                var password = "password";

                // Add admin user
                var users = new List<User>
                {
                    new User
                    {
                        UserName = adminUserName,
                        Name = adminName,
                        Role = UserRole.SUPERADMIN
                    }
                };

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    // Add test users
                    for (int i = 1; i <= 6; i++)
                    {
                        users.Add(new User
                        {
                            UserName = $"test{i}@test.com",
                            Name = $"test{i}",
                            Role = UserRole.USER
                        });
                    }
                }

                foreach (var user in users)
                {
                    if (user.Role == UserRole.SUPERADMIN)
                    {
                        userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();
                        userManager.AddToRoleAsync(user, "Superadmin").GetAwaiter().GetResult();
                    }

                    if (user.Role == UserRole.ADMIN)
                    {
                        userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();
                        userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
                    }

                    if (user.Role == UserRole.USER)
                    {
                        userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                        userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
                    }
                }

            }
        }


    }
}
