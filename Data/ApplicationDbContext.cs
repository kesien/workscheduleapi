﻿using beosztas_api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<Request>().Navigation(request => request.User).AutoInclude();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<MonthlySchedule> Schedules { get; set; }
        public DbSet<MorningSchedule> MorningSchedules { get; set; }
        public DbSet<Forenoonschedule> ForenoonSchedules { get; set; }
        public DbSet<HolidaySchedule> HolidaySchedules { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        
        

        public IQueryable<Request> RequestsWithUsers => this.Requests.Include(x => x.User);
    }
}