using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Days_DayId2",
                table: "Users");

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
                keyValue: "42aaec23-7e28-4247-8011-707d810e28c7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "549ac828-6b72-4874-b91c-03d078b134aa");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayId2",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Days",
                newName: "MonhtlyScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_ScheduleId",
                table: "Days",
                newName: "IX_Days_MonhtlyScheduleId");

            migrationBuilder.CreateTable(
                name: "Forenoonschedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forenoonschedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forenoonschedule_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forenoonschedule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidaySchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaySchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidaySchedule_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HolidaySchedule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MorningSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorningSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MorningSchedule_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MorningSchedule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8044230-92c1-4fd1-af6f-12ee5dc9880e", "fc619503-6669-4b8e-a4a4-e0d8e026ab90", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec411b68-09cf-4115-8715-5b55d5da9e10", "029f0b5c-bc18-4130-affa-4776e9bd40a9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Forenoonschedule_DayId",
                table: "Forenoonschedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Forenoonschedule_UserId",
                table: "Forenoonschedule",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedule_DayId",
                table: "HolidaySchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedule_UserId",
                table: "HolidaySchedule",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedule_DayId",
                table: "MorningSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedule_UserId",
                table: "MorningSchedule",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_MonhtlyScheduleId",
                table: "Days",
                column: "MonhtlyScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_MonhtlyScheduleId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "Forenoonschedule");

            migrationBuilder.DropTable(
                name: "HolidaySchedule");

            migrationBuilder.DropTable(
                name: "MorningSchedule");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d8044230-92c1-4fd1-af6f-12ee5dc9880e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ec411b68-09cf-4115-8715-5b55d5da9e10");

            migrationBuilder.RenameColumn(
                name: "MonhtlyScheduleId",
                table: "Days",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_MonhtlyScheduleId",
                table: "Days",
                newName: "IX_Days_ScheduleId");

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

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42aaec23-7e28-4247-8011-707d810e28c7", "92b11133-f1c5-4d9d-afc4-ff264879a0af", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "549ac828-6b72-4874-b91c-03d078b134aa", "758827ee-391f-441b-b268-1b24bd88964f", "Administrator", "ADMINISTRATOR" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

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
    }
}
