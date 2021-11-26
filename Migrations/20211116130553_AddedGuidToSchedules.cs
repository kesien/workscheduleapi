using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedGuidToSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForenoonSchedules_Days_DayId",
                table: "ForenoonSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidaySchedules_Days_DayId",
                table: "HolidaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MorningSchedules_Days_DayId",
                table: "MorningSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MorningSchedules_DayId",
                table: "MorningSchedules");

            migrationBuilder.DropIndex(
                name: "IX_HolidaySchedules_DayId",
                table: "HolidaySchedules");

            migrationBuilder.DropIndex(
                name: "IX_ForenoonSchedules_DayId",
                table: "ForenoonSchedules");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5fe8beff-c7a4-4d62-beb9-87b942109a26");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "82001550-8da4-413e-95a9-51729d831988");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "MorningSchedules");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "HolidaySchedules");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "ForenoonSchedules");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Schedules",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyScheduleId",
                table: "Days",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DayId = table.Column<int>(type: "INTEGER", nullable: true),
                    DayId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    DayId2 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSchedule_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedule_Days_DayId1",
                        column: x => x.DayId1,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedule_Days_DayId2",
                        column: x => x.DayId2,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93f05df4-e32c-4161-a32a-3df75bd7167a", "1bd5270c-42ea-4dc8-8833-1b334c7adfc7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2658559-7e15-4f94-ab2b-16477d5c5736", "d0563dca-0c63-4f2a-a55c-66da510c49d5", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_DayId",
                table: "UserSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_DayId1",
                table: "UserSchedule",
                column: "DayId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_DayId2",
                table: "UserSchedule",
                column: "DayId2");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_UserId",
                table: "UserSchedule",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSchedule");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "93f05df4-e32c-4161-a32a-3df75bd7167a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d2658559-7e15-4f94-ab2b-16477d5c5736");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "MorningSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "HolidaySchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "ForenoonSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MonthlyScheduleId",
                table: "Days",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fe8beff-c7a4-4d62-beb9-87b942109a26", "00d84a80-73ed-4d20-91b0-c008176575c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82001550-8da4-413e-95a9-51729d831988", "7ddb47eb-4cdf-48cc-b4fb-d6d5f9fe901d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedules_DayId",
                table: "MorningSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedules_DayId",
                table: "HolidaySchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ForenoonSchedules_DayId",
                table: "ForenoonSchedules",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForenoonSchedules_Days_DayId",
                table: "ForenoonSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidaySchedules_Days_DayId",
                table: "HolidaySchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedules_Days_DayId",
                table: "MorningSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");
        }
    }
}
