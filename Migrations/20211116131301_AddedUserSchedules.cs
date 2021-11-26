using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedUserSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Days_DayId",
                table: "UserSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Days_DayId1",
                table: "UserSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Days_DayId2",
                table: "UserSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Users_UserId",
                table: "UserSchedule");

            migrationBuilder.DropTable(
                name: "ForenoonSchedules");

            migrationBuilder.DropTable(
                name: "HolidaySchedules");

            migrationBuilder.DropTable(
                name: "MorningSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "93f05df4-e32c-4161-a32a-3df75bd7167a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d2658559-7e15-4f94-ab2b-16477d5c5736");

            migrationBuilder.RenameTable(
                name: "UserSchedule",
                newName: "UserSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_UserId",
                table: "UserSchedules",
                newName: "IX_UserSchedules_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_DayId2",
                table: "UserSchedules",
                newName: "IX_UserSchedules_DayId2");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_DayId1",
                table: "UserSchedules",
                newName: "IX_UserSchedules_DayId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_DayId",
                table: "UserSchedules",
                newName: "IX_UserSchedules_DayId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Days",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DayId2",
                table: "UserSchedules",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DayId1",
                table: "UserSchedules",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DayId",
                table: "UserSchedules",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedules",
                table: "UserSchedules",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e8935d2-5129-438a-948e-f6392d73f1a0", "3d023fe8-0d0c-4b8f-ba79-2c5665df184d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50abccb4-5e9e-4171-abd4-480c274e9b0b", "a6929898-ce17-4a73-99f8-e95c2dafc3a2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Days_DayId",
                table: "UserSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Days_DayId1",
                table: "UserSchedules",
                column: "DayId1",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Days_DayId2",
                table: "UserSchedules",
                column: "DayId2",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Users_UserId",
                table: "UserSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Days_DayId",
                table: "UserSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Days_DayId1",
                table: "UserSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Days_DayId2",
                table: "UserSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Users_UserId",
                table: "UserSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedules",
                table: "UserSchedules");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0e8935d2-5129-438a-948e-f6392d73f1a0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "50abccb4-5e9e-4171-abd4-480c274e9b0b");

            migrationBuilder.RenameTable(
                name: "UserSchedules",
                newName: "UserSchedule");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedules_UserId",
                table: "UserSchedule",
                newName: "IX_UserSchedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedules_DayId2",
                table: "UserSchedule",
                newName: "IX_UserSchedule_DayId2");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedules_DayId1",
                table: "UserSchedule",
                newName: "IX_UserSchedule_DayId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedules_DayId",
                table: "UserSchedule",
                newName: "IX_UserSchedule_DayId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "DayId2",
                table: "UserSchedule",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayId1",
                table: "UserSchedule",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "UserSchedule",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ForenoonSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForenoonSchedules", x => x.Id);
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaySchedules", x => x.Id);
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorningSchedules", x => x.Id);
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
                values: new object[] { "93f05df4-e32c-4161-a32a-3df75bd7167a", "1bd5270c-42ea-4dc8-8833-1b334c7adfc7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2658559-7e15-4f94-ab2b-16477d5c5736", "d0563dca-0c63-4f2a-a55c-66da510c49d5", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_ForenoonSchedules_UserId",
                table: "ForenoonSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidaySchedules_UserId",
                table: "HolidaySchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MorningSchedules_UserId",
                table: "MorningSchedules",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Days_DayId",
                table: "UserSchedule",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Days_DayId1",
                table: "UserSchedule",
                column: "DayId1",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Days_DayId2",
                table: "UserSchedule",
                column: "DayId2",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Users_UserId",
                table: "UserSchedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
