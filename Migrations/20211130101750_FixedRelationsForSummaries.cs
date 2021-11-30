using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkScheduleMaker.Migrations
{
    public partial class FixedRelationsForSummaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Summaries_Schedules_MonthlyScheduleId",
                table: "Summaries");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3df7a835-afc8-4846-b546-2e5602a7508b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f2da4b5a-a0c9-40e0-a37d-7ec9d75286ec");

            migrationBuilder.DropColumn(
                name: "MonhtlyScheduleId",
                table: "Summaries");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyScheduleId",
                table: "Summaries",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4079f6cf-2ab2-46f8-9b62-7dbae7d1fd43", "1a5f0f46-73d1-46e0-991b-2ce2a3485a2c", "User", "USER" },
                    { "9d5d3bb0-45ca-4479-bb9b-c43d07491d60", "8767bc0e-4bd8-49d0-bcaa-9468fc2adcbf", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Summaries_Schedules_MonthlyScheduleId",
                table: "Summaries",
                column: "MonthlyScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Summaries_Schedules_MonthlyScheduleId",
                table: "Summaries");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4079f6cf-2ab2-46f8-9b62-7dbae7d1fd43");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9d5d3bb0-45ca-4479-bb9b-c43d07491d60");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyScheduleId",
                table: "Summaries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonhtlyScheduleId",
                table: "Summaries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3df7a835-afc8-4846-b546-2e5602a7508b", "e924cfd3-d4d5-4edf-b7b7-f35edf235fd8", "Administrator", "ADMINISTRATOR" },
                    { "f2da4b5a-a0c9-40e0-a37d-7ec9d75286ec", "7f0d92e5-dd53-4a9d-8d2f-8e9dd7a11dbe", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Summaries_Schedules_MonthlyScheduleId",
                table: "Summaries",
                column: "MonthlyScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
