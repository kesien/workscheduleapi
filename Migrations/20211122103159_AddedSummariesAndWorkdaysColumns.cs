using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedSummariesAndWorkdaysColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "291b6a38-4750-4f11-bb0a-4316a67590ab");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a72a6a8f-a301-4d17-83ae-76495cc7285a");

            migrationBuilder.AddColumn<int>(
                name: "NumOfWorkdays",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MonthlyScheduleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MonhtlyScheduleId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Morning = table.Column<int>(type: "INTEGER", nullable: false),
                    Forenoon = table.Column<int>(type: "INTEGER", nullable: false),
                    Holiday = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Summaries_Schedules_MonthlyScheduleId",
                        column: x => x.MonthlyScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36f1fcc6-b04b-408f-8a08-8aa1514be181", "26c27ddd-d360-4f72-bee0-0e1049ab90a7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2389335-5062-4998-a3fc-29a830e2a510", "bfc7f982-aadf-46a3-80a0-613f9ed86acd", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_MonthlyScheduleId",
                table: "Summaries",
                column: "MonthlyScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Summaries");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "36f1fcc6-b04b-408f-8a08-8aa1514be181");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a2389335-5062-4998-a3fc-29a830e2a510");

            migrationBuilder.DropColumn(
                name: "NumOfWorkdays",
                table: "Schedules");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "291b6a38-4750-4f11-bb0a-4316a67590ab", "952703b0-6f97-4a6e-9af2-2baabeb626b1", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a72a6a8f-a301-4d17-83ae-76495cc7285a", "50f2d934-1525-4ba1-acfb-d9a9b856413b", "Administrator", "ADMINISTRATOR" });
        }
    }
}
