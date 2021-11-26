using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule5 : Migration
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

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c8d188b4-8237-40f8-b4c2-6dc495069241");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f988951d-de21-429c-a5a2-c01dd24c5700");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "MorningSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "HolidaySchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "ForenoonSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4273cc0d-e578-4fce-aef0-b6a304dd39af", "00f9ae06-9d1c-4062-918c-804fcc9f328b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a96657b6-29d4-466a-95b1-1a97ddb2ec44", "cbf82963-4f32-415d-8774-64ee66651b78", "User", "USER" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4273cc0d-e578-4fce-aef0-b6a304dd39af");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a96657b6-29d4-466a-95b1-1a97ddb2ec44");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "MorningSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "HolidaySchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "ForenoonSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
                name: "FK_HolidaySchedules_Days_DayId",
                table: "HolidaySchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MorningSchedules_Days_DayId",
                table: "MorningSchedules",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
