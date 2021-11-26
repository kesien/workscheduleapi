using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedIsRequestColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b075c08c-1481-4345-9b2c-9e234ad734db");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b6b5810c-a832-4310-9e81-0164cde8f3c7");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequest",
                table: "MorningSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequest",
                table: "ForenoonSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e22fc77-5b2e-4d43-a6f9-f2848ecd53dc", "f7c54039-3688-40f4-b864-2d0675c142c7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b17a95a-7127-4c95-a719-883272b8d194", "862333dd-d46b-47ae-993a-2796a061db71", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0e22fc77-5b2e-4d43-a6f9-f2848ecd53dc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8b17a95a-7127-4c95-a719-883272b8d194");

            migrationBuilder.DropColumn(
                name: "IsRequest",
                table: "MorningSchedules");

            migrationBuilder.DropColumn(
                name: "IsRequest",
                table: "ForenoonSchedules");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b075c08c-1481-4345-9b2c-9e234ad734db", "dc8645c1-53fa-4e38-8f74-9a9d5a238331", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6b5810c-a832-4310-9e81-0164cde8f3c7", "67490d16-1449-4a15-9d26-8a6db0b2e678", "User", "USER" });
        }
    }
}
