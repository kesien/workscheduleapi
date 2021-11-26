using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class SeparatedIsHolidayAndIsWeekend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "254666a1-1439-4fa0-badf-abb58af18690");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "83abb1b1-94e9-4d45-9542-7441b15ab1f7");

            migrationBuilder.RenameColumn(
                name: "IsHolidayOrWeekend",
                table: "Days",
                newName: "IsWeekend");

            migrationBuilder.AddColumn<bool>(
                name: "IsHoliday",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b075c08c-1481-4345-9b2c-9e234ad734db", "dc8645c1-53fa-4e38-8f74-9a9d5a238331", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6b5810c-a832-4310-9e81-0164cde8f3c7", "67490d16-1449-4a15-9d26-8a6db0b2e678", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b075c08c-1481-4345-9b2c-9e234ad734db");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b6b5810c-a832-4310-9e81-0164cde8f3c7");

            migrationBuilder.DropColumn(
                name: "IsHoliday",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "IsWeekend",
                table: "Days",
                newName: "IsHolidayOrWeekend");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "254666a1-1439-4fa0-badf-abb58af18690", "11cc03ad-2715-4bb1-920e-fd00bf693c2e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83abb1b1-94e9-4d45-9542-7441b15ab1f7", "9059d5f2-579e-4842-889a-039ddfe25527", "User", "USER" });
        }
    }
}
