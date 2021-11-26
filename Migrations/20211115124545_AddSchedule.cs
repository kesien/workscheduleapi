using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "068fc9a6-6ed5-4055-b13a-e2e380dec1f6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1d3d487c-d856-4d3a-860b-451456020cdd");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId1",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId2",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsHolidayOrWeekend = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a993f02-702f-4f40-8a66-fbe520dd5854", "2dd893cf-9391-4264-8e0a-d342e454fae0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49fa81d3-db8e-4ec2-b40e-90afa046c210", "96046a11-40a6-4802-99af-ece8179e2003", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DayId",
                table: "Users",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DayId1",
                table: "Users",
                column: "DayId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DayId2",
                table: "Users",
                column: "DayId2");

            migrationBuilder.CreateIndex(
                name: "IX_Days_ScheduleId",
                table: "Days",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Days_DayId",
                table: "Users",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Days_DayId1",
                table: "Users",
                column: "DayId1",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Days_DayId2",
                table: "Users",
                column: "DayId2",
                principalTable: "Days",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId2",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Users_DayId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DayId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DayId2",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2a993f02-702f-4f40-8a66-fbe520dd5854");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "49fa81d3-db8e-4ec2-b40e-90afa046c210");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayId2",
                table: "Users");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "068fc9a6-6ed5-4055-b13a-e2e380dec1f6", "21199fd3-c331-44c5-972b-7ebe0b1a0f86", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d3d487c-d856-4d3a-860b-451456020cdd", "6585dc4d-a27f-4e2e-b5cd-3b7a8b3a0f7f", "User", "USER" });
        }
    }
}
