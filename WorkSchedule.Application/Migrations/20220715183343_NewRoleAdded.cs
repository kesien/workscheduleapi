using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSchedule.Application.Migrations
{
    public partial class NewRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1da58f4d-44e9-4460-b4b9-3877481affb1"),
                column: "ConcurrencyStamp",
                value: "64ee1966-e11d-45d7-96fb-8b765a6fafe2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ffc59f07-0034-4f83-b673-f21da9179c9d"),
                column: "ConcurrencyStamp",
                value: "df10de11-d84a-4ac3-a3e9-ac8b972b806e");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3a1a2c1e-d7ee-4cbc-b054-9610f6d851a2"), "384b5ebf-6d3d-4ffe-84b4-5d9f92a3956c", "Superadmin", "SUPERADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3a1a2c1e-d7ee-4cbc-b054-9610f6d851a2"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1da58f4d-44e9-4460-b4b9-3877481affb1"),
                column: "ConcurrencyStamp",
                value: "ced1f067-05a8-4007-b959-c994b1b9cf98");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ffc59f07-0034-4f83-b673-f21da9179c9d"),
                column: "ConcurrencyStamp",
                value: "0696fc63-0f91-4091-b15d-65ae8aee423d");
        }
    }
}
