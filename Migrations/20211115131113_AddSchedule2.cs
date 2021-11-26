using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class AddSchedule2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2a993f02-702f-4f40-8a66-fbe520dd5854");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "49fa81d3-db8e-4ec2-b40e-90afa046c210");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Days",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42aaec23-7e28-4247-8011-707d810e28c7", "92b11133-f1c5-4d9d-afc4-ff264879a0af", "User", "USER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "549ac828-6b72-4874-b91c-03d078b134aa", "758827ee-391f-441b-b268-1b24bd88964f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "42aaec23-7e28-4247-8011-707d810e28c7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "549ac828-6b72-4874-b91c-03d078b134aa");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a993f02-702f-4f40-8a66-fbe520dd5854", "2dd893cf-9391-4264-8e0a-d342e454fae0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49fa81d3-db8e-4ec2-b40e-90afa046c210", "96046a11-40a6-4802-99af-ece8179e2003", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Schedules_ScheduleId",
                table: "Days",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
