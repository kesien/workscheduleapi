using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class RequestChangedPrimaryKeyToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0e22fc77-5b2e-4d43-a6f9-f2848ecd53dc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8b17a95a-7127-4c95-a719-883272b8d194");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Requests",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "291b6a38-4750-4f11-bb0a-4316a67590ab", "952703b0-6f97-4a6e-9af2-2baabeb626b1", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a72a6a8f-a301-4d17-83ae-76495cc7285a", "50f2d934-1525-4ba1-acfb-d9a9b856413b", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "291b6a38-4750-4f11-bb0a-4316a67590ab");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a72a6a8f-a301-4d17-83ae-76495cc7285a");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Requests",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e22fc77-5b2e-4d43-a6f9-f2848ecd53dc", "f7c54039-3688-40f4-b864-2d0675c142c7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b17a95a-7127-4c95-a719-883272b8d194", "862333dd-d46b-47ae-993a-2796a061db71", "User", "USER" });
        }
    }
}
