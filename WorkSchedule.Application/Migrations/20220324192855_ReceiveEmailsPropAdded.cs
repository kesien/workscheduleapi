using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSchedule.Application.Migrations
{
    public partial class ReceiveEmailsPropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceiveEmails",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveEmails",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1da58f4d-44e9-4460-b4b9-3877481affb1"),
                column: "ConcurrencyStamp",
                value: "1c909ef3-a88e-4dc8-8ecb-50e92807ea81");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ffc59f07-0034-4f83-b673-f21da9179c9d"),
                column: "ConcurrencyStamp",
                value: "c59313f3-ea83-4ec3-bf06-bed71db3ecff");
        }
    }
}
