using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedDifferentScheduleTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSchedules");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0e8935d2-5129-438a-948e-f6392d73f1a0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "50abccb4-5e9e-4171-abd4-480c274e9b0b");

            migrationBuilder.CreateTable(
                name: "ForenoonSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DayId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForenoonSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForenoonSchedules_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ForenoonSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidaySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DayId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidaySchedules_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HolidaySchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MorningSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DayId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorningSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MorningSchedules_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MorningSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "254666a1-1439-4fa0-badf-abb58af18690", "11cc03ad-2715-4bb1-920e-fd00bf693c2e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83abb1b1-94e9-4d45-9542-7441b15ab1f7", "9059d5f2-579e-4842-889a-039ddfe25527", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_ForenoonSchedules_DayId",
                table: "ForenoonSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ForenoonSchedules_UserId",
                table: "ForenoonSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedules_DayId",
                table: "HolidaySchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedules_UserId",
                table: "HolidaySchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedules_DayId",
                table: "MorningSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedules_UserId",
                table: "MorningSchedules",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForenoonSchedules");

            migrationBuilder.DropTable(
                name: "HolidaySchedules");

            migrationBuilder.DropTable(
                name: "MorningSchedules");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "254666a1-1439-4fa0-badf-abb58af18690");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "83abb1b1-94e9-4d45-9542-7441b15ab1f7");

            migrationBuilder.CreateTable(
                name: "UserSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DayId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DayId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    DayId2 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSchedules_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedules_Days_DayId1",
                        column: x => x.DayId1,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedules_Days_DayId2",
                        column: x => x.DayId2,
                        principalTable: "Days",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e8935d2-5129-438a-948e-f6392d73f1a0", "3d023fe8-0d0c-4b8f-ba79-2c5665df184d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50abccb4-5e9e-4171-abd4-480c274e9b0b", "a6929898-ce17-4a73-99f8-e95c2dafc3a2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedules_DayId",
                table: "UserSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedules_DayId1",
                table: "UserSchedules",
                column: "DayId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedules_DayId2",
                table: "UserSchedules",
                column: "DayId2");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedules_UserId",
                table: "UserSchedules",
                column: "UserId");
        }
    }
}
