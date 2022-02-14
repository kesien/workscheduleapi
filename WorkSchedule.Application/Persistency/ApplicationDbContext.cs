using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Persistency
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var property in builder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
                property.SetColumnType("timestamp without time zone");
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<MonthlySchedule>().HasMany(e => e.Summaries).WithOne(e => e.MonthlySchedule).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<MonthlySchedule>().HasOne(e => e.WordFile).WithOne(e => e.MonthlySchedule).HasForeignKey<WordFile>(e => e.MonthlyScheduleId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(e => e.ForenoonSchedules).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(e => e.MorningSchedules).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(e => e.HolidaySchedules).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Request>? Requests { get; set; }
        public DbSet<Day>? Days { get; set; }
        public DbSet<MonthlySchedule>? Schedules { get; set; }
        public DbSet<MorningSchedule>? MorningSchedules { get; set; }
        public DbSet<Forenoonschedule>? ForenoonSchedules { get; set; }
        public DbSet<HolidaySchedule>? HolidaySchedules { get; set; }
        public DbSet<Summary>? Summaries { get; set; }
        public DbSet<Holiday>? Holidays { get; set; }
    }
}
