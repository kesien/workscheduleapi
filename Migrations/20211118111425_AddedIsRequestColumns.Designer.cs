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
    [Migration("20211118111425_AddedIsRequestColumns")]
    partial class AddedIsRequestColumns
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
                            Id = "8b17a95a-7127-4c95-a719-883272b8d194",
                            ConcurrencyStamp = "862333dd-d46b-47ae-993a-2796a061db71",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "0e22fc77-5b2e-4d43-a6f9-f2848ecd53dc",
                            ConcurrencyStamp = "f7c54039-3688-40f4-b864-2d0675c142c7",
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

                    b.Property<bool>("IsRequest")
                        .HasColumnType("INTEGER");

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

                    b.Property<bool>("IsRequest")
                        .HasColumnType("INTEGER");

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