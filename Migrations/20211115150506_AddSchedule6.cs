using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4273cc0d-e578-4fce-aef0-b6a304dd39af");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a96657b6-29d4-466a-95b1-1a97ddb2ec44");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ef73257-2fb8-49d9-9f34-ed461d3b8b24", "f5ad880a-62e6-488a-b4f0-2933944fbe67", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a86a5ac2-9f89-45ef-b26f-bd646f6c8afc", "6f8e570e-8632-4022-a84b-01a169869310", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6ef73257-2fb8-49d9-9f34-ed461d3b8b24");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a86a5ac2-9f89-45ef-b26f-bd646f6c8afc");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4273cc0d-e578-4fce-aef0-b6a304dd39af", "00f9ae06-9d1c-4062-918c-804fcc9f328b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a96657b6-29d4-466a-95b1-1a97ddb2ec44", "cbf82963-4f32-415d-8774-64ee66651b78", "User", "USER" });
        }
    }
}
