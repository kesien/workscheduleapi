﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkSchedule.Application.Persistency;

#nullable disable

namespace WorkSchedule.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("IdentityUserRole<Guid>");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsHoliday")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWeekend")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MonthlyScheduleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyScheduleId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Forenoonschedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRequest")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("ForenoonSchedules");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Holiday", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFix")
                        .HasColumnType("boolean");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.HolidaySchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("HolidaySchedules");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.MonthlySchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsSaved")
                        .HasColumnType("boolean");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfWorkdays")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.MorningSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRequest")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("MorningSchedules");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffc59f07-0034-4f83-b673-f21da9179c9d"),
                            ConcurrencyStamp = "c59313f3-ea83-4ec3-bf06-bed71db3ecff",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("1da58f4d-44e9-4460-b4b9-3877481affb1"),
                            ConcurrencyStamp = "1c909ef3-a88e-4dc8-8ecb-50e92807ea81",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Summary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Forenoon")
                        .HasColumnType("integer");

                    b.Property<int>("Holiday")
                        .HasColumnType("integer");

                    b.Property<Guid>("MonthlyScheduleId")
                        .HasColumnType("uuid");

                    b.Property<int>("Morning")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyScheduleId");

                    b.ToTable("Summaries");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.WordFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MonthlyScheduleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyScheduleId")
                        .IsUnique();

                    b.ToTable("WordFile");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Day", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.MonthlySchedule", "MonthlySchedule")
                        .WithMany("Days")
                        .HasForeignKey("MonthlyScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlySchedule");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Forenoonschedule", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.Day", "Day")
                        .WithMany("UsersScheduledForForenoon")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkSchedule.Application.Persistency.Entities.User", "User")
                        .WithMany("ForenoonSchedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.HolidaySchedule", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.Day", "Day")
                        .WithMany("UsersOnHoliday")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkSchedule.Application.Persistency.Entities.User", "User")
                        .WithMany("HolidaySchedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.MorningSchedule", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.Day", "Day")
                        .WithMany("UsersScheduledForMorning")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkSchedule.Application.Persistency.Entities.User", "User")
                        .WithMany("MorningSchedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Request", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Summary", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.MonthlySchedule", "MonthlySchedule")
                        .WithMany("Summaries")
                        .HasForeignKey("MonthlyScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlySchedule");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.WordFile", b =>
                {
                    b.HasOne("WorkSchedule.Application.Persistency.Entities.MonthlySchedule", "MonthlySchedule")
                        .WithOne("WordFile")
                        .HasForeignKey("WorkSchedule.Application.Persistency.Entities.WordFile", "MonthlyScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlySchedule");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.Day", b =>
                {
                    b.Navigation("UsersOnHoliday");

                    b.Navigation("UsersScheduledForForenoon");

                    b.Navigation("UsersScheduledForMorning");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.MonthlySchedule", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("Summaries");

                    b.Navigation("WordFile");
                });

            modelBuilder.Entity("WorkSchedule.Application.Persistency.Entities.User", b =>
                {
                    b.Navigation("ForenoonSchedules");

                    b.Navigation("HolidaySchedules");

                    b.Navigation("MorningSchedules");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
