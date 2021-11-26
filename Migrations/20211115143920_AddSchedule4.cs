using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forenoonschedule_Days_DayId",
                table: "Forenoonschedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Forenoonschedule_Users_UserId",
                table: "Forenoonschedule");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidaySchedule_Days_DayId",
                table: "HolidaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidaySchedule_Users_UserId",
                table: "HolidaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_MorningSchedule_Days_DayId",
                table: "MorningSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_MorningSchedule_Users_UserId",
                table: "MorningSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MorningSchedule",
                table: "MorningSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidaySchedule",
                table: "HolidaySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forenoonschedule",
                table: "Forenoonschedule");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d8044230-92c1-4fd1-af6f-12ee5dc9880e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ec411b68-09cf-4115-8715-5b55d5da9e10");

            migrationBuilder.RenameTable(
                name: "MorningSchedule",
                newName: "MorningSchedules");

            migrationBuilder.RenameTable(
                name: "HolidaySchedule",
                newName: "HolidaySchedules");

            migrationBuilder.RenameTable(
                name: "Forenoonschedule",
                newName: "ForenoonSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_MorningSchedule_UserId",
                table: "MorningSchedules",
                newName: "IX_MorningSchedules_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MorningSchedule_DayId",
                table: "MorningSchedules",
                newName: "IX_MorningSchedules_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidaySchedule_UserId",
                table: "HolidaySchedules",
                newName: "IX_HolidaySchedules_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidaySchedule_DayId",
                table: "HolidaySchedules",
                newName: "IX_HolidaySchedules_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_Forenoonschedule_UserId",
                table: "ForenoonSchedules",
                newName: "IX_ForenoonSchedules_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Forenoonschedule_DayId",
                table: "ForenoonSchedules",
                newName: "IX_ForenoonSchedules_DayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MorningSchedules",
                table: "MorningSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidaySchedules",
                table: "HolidaySchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForenoonSchedules",
                table: "ForenoonSchedules",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8d188b4-8237-40f8-b4c2-6dc495069241", "cf5f77c0-5373-47b2-aab2-8303d987d574", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f988951d-de21-429c-a5a2-c01dd24c5700", "5f032905-a47f-4c31-b95e-29ea9ec339d2", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_ForenoonSchedules_Days_DayId",
                table: "ForenoonSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForenoonSchedules_Users_UserId",
                table: "ForenoonSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidaySchedules_Days_DayId",
                table: "HolidaySchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidaySchedules_Users_UserId",
                table: "HolidaySchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedules_Days_DayId",
                table: "MorningSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedules_Users_UserId",
                table: "MorningSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForenoonSchedules_Days_DayId",
                table: "ForenoonSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ForenoonSchedules_Users_UserId",
                table: "ForenoonSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidaySchedules_Days_DayId",
                table: "HolidaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_HolidaySchedules_Users_UserId",
                table: "HolidaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MorningSchedules_Days_DayId",
                table: "MorningSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MorningSchedules_Users_UserId",
                table: "MorningSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MorningSchedules",
                table: "MorningSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HolidaySchedules",
                table: "HolidaySchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForenoonSchedules",
                table: "ForenoonSchedules");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c8d188b4-8237-40f8-b4c2-6dc495069241");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f988951d-de21-429c-a5a2-c01dd24c5700");

            migrationBuilder.RenameTable(
                name: "MorningSchedules",
                newName: "MorningSchedule");

            migrationBuilder.RenameTable(
                name: "HolidaySchedules",
                newName: "HolidaySchedule");

            migrationBuilder.RenameTable(
                name: "ForenoonSchedules",
                newName: "Forenoonschedule");

            migrationBuilder.RenameIndex(
                name: "IX_MorningSchedules_UserId",
                table: "MorningSchedule",
                newName: "IX_MorningSchedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MorningSchedules_DayId",
                table: "MorningSchedule",
                newName: "IX_MorningSchedule_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidaySchedules_UserId",
                table: "HolidaySchedule",
                newName: "IX_HolidaySchedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidaySchedules_DayId",
                table: "HolidaySchedule",
                newName: "IX_HolidaySchedule_DayId");

            migrationBuilder.RenameIndex(
                name: "IX_ForenoonSchedules_UserId",
                table: "Forenoonschedule",
                newName: "IX_Forenoonschedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForenoonSchedules_DayId",
                table: "Forenoonschedule",
                newName: "IX_Forenoonschedule_DayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MorningSchedule",
                table: "MorningSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolidaySchedule",
                table: "HolidaySchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forenoonschedule",
                table: "Forenoonschedule",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8044230-92c1-4fd1-af6f-12ee5dc9880e", "fc619503-6669-4b8e-a4a4-e0d8e026ab90", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec411b68-09cf-4115-8715-5b55d5da9e10", "029f0b5c-bc18-4130-affa-4776e9bd40a9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Forenoonschedule_Days_DayId",
                table: "Forenoonschedule",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forenoonschedule_Users_UserId",
                table: "Forenoonschedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidaySchedule_Days_DayId",
                table: "HolidaySchedule",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidaySchedule_Users_UserId",
                table: "HolidaySchedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedule_Days_DayId",
                table: "MorningSchedule",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedule_Users_UserId",
                table: "MorningSchedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
