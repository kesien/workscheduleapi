using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSchedule.Application.Migrations
{
    public partial class AddedWordFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2c47da92-98ea-46b7-aed4-bc7b0441e5e6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8ced8ec3-e8c7-4e86-ae96-25cb8a456bcf");

            migrationBuilder.CreateTable(
                name: "WordFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    MonthlyScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordFile_Schedules_MonthlyScheduleId",
                        column: x => x.MonthlyScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03edea94-0d85-4472-8e3d-d45d97420b41", "f77d5aff-a8ba-4f89-8e40-5fb3881b4c55", "User", "USER" },
                    { "75fc1c44-e82d-4d0c-9564-826bd060236a", "b3837de0-0ef9-43a1-a90d-e0468f220e9e", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordFile_MonthlyScheduleId",
                table: "WordFile",
                column: "MonthlyScheduleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordFile");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "03edea94-0d85-4472-8e3d-d45d97420b41");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "75fc1c44-e82d-4d0c-9564-826bd060236a");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c47da92-98ea-46b7-aed4-bc7b0441e5e6", "15ebd0f0-d64a-4d71-a8ba-53c5f68fb1cd", "User", "USER" },
                    { "8ced8ec3-e8c7-4e86-ae96-25cb8a456bcf", "16770340-c4d9-45f2-bf5e-e2af4a747120", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
