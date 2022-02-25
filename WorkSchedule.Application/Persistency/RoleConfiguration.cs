using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Persistency
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
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
            );
        }
    }
}
