using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_MonhtlyScheduleId",
                table: "Days");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6ef73257-2fb8-49d9-9f34-ed461d3b8b24");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a86a5ac2-9f89-45ef-b26f-bd646f6c8afc");

            migrationBuilder.RenameColumn(
                name: "MonhtlyScheduleId",
                table: "Days",
                newName: "MonthlyScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_MonhtlyScheduleId",
                table: "Days",
                newName: "IX_Days_MonthlyScheduleId");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fe8beff-c7a4-4d62-beb9-87b942109a26", "00d84a80-73ed-4d20-91b0-c008176575c8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82001550-8da4-413e-95a9-51729d831988", "7ddb47eb-4cdf-48cc-b4fb-d6d5f9fe901d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_MonthlyScheduleId",
                table: "Days",
                column: "MonthlyScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_MonthlyScheduleId",
                table: "Days");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5fe8beff-c7a4-4d62-beb9-87b942109a26");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "82001550-8da4-413e-95a9-51729d831988");

            migrationBuilder.RenameColumn(
                name: "MonthlyScheduleId",
                table: "Days",
                newName: "MonhtlyScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_MonthlyScheduleId",
                table: "Days",
                newName: "IX_Days_MonhtlyScheduleId");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ef73257-2fb8-49d9-9f34-ed461d3b8b24", "f5ad880a-62e6-488a-b4f0-2933944fbe67", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a86a5ac2-9f89-45ef-b26f-bd646f6c8afc", "6f8e570e-8632-4022-a84b-01a169869310", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_MonhtlyScheduleId",
                table: "Days",
                column: "MonhtlyScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
