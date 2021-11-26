﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkScheduleMaker.Data;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211118110102_SeparatedIsHolidayAndIsWeekend")]
    partial class SeparatedIsHolidayAndIsWeekend
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "b6b5810c-a832-4310-9e81-0164cde8f3c7",
                            ConcurrencyStamp = "67490d16-1449-4a15-9d26-8a6db0b2e678",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "b075c08c-1481-4345-9b2c-9e234ad734db",
                            ConcurrencyStamp = "dc8645c1-53fa-4e38-8f74-9a9d5a238331",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("IdentityUserRole<string>");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHoliday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWeekend")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("MonthlyScheduleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyScheduleId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Forenoonschedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("DayId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("ForenoonSchedules");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.HolidaySchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("DayId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("HolidaySchedules");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.MonthlySchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.MorningSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("DayId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("MorningSchedules");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Day", b =>
                {
                    b.HasOne("WorkScheduleMaker.Entities.MonthlySchedule", null)
                        .WithMany("Days")
                        .HasForeignKey("MonthlyScheduleId");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Forenoonschedule", b =>
                {
                    b.HasOne("WorkScheduleMaker.Entities.Day", null)
                        .WithMany("UsersScheduledForForenoon")
                        .HasForeignKey("DayId");

                    b.HasOne("WorkScheduleMaker.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.HolidaySchedule", b =>
                {
                    b.HasOne("WorkScheduleMaker.Entities.Day", null)
                        .WithMany("UsersOnHoliday")
                        .HasForeignKey("DayId");

                    b.HasOne("WorkScheduleMaker.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.MorningSchedule", b =>
                {
                    b.HasOne("WorkScheduleMaker.Entities.Day", null)
                        .WithMany("UsersScheduledForMorning")
                        .HasForeignKey("DayId");

                    b.HasOne("WorkScheduleMaker.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Request", b =>
                {
                    b.HasOne("WorkScheduleMaker.Entities.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.Day", b =>
                {
                    b.Navigation("UsersOnHoliday");

                    b.Navigation("UsersScheduledForForenoon");

                    b.Navigation("UsersScheduledForMorning");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.MonthlySchedule", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("WorkScheduleMaker.Entities.User", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}