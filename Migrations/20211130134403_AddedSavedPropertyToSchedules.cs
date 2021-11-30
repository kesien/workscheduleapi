using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddedSavedPropertyToSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4079f6cf-2ab2-46f8-9b62-7dbae7d1fd43");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9d5d3bb0-45ca-4479-bb9b-c43d07491d60");

            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70873a1e-3c18-480b-ae27-925fb1763ea0", "989dda94-27f6-4478-8566-c0d1472760ca", "User", "USER" },
                    { "aa2ba727-d809-4813-8412-aaa0a1ec231c", "f342cd32-900a-412a-adf0-d92dd5e578c0", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "70873a1e-3c18-480b-ae27-925fb1763ea0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "aa2ba727-d809-4813-8412-aaa0a1ec231c");

            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "Schedules");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4079f6cf-2ab2-46f8-9b62-7dbae7d1fd43", "1a5f0f46-73d1-46e0-991b-2ce2a3485a2c", "User", "USER" },
                    { "9d5d3bb0-45ca-4479-bb9b-c43d07491d60", "8767bc0e-4bd8-49d0-bcaa-9468fc2adcbf", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
