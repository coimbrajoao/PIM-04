using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class CorrigindoPDFdafolhaeadicionandomaisinformaçoesnorelogio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TimeClock_TimeClockIdTimeclock",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TimeClockIdTimeclock",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeClockIdTimeclock",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeOffset",
                table: "TimeClock");

            migrationBuilder.AddColumn<string>(
                name: "Office",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsClockIn",
                table: "TimeClock",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Office",
                table: "Payrolls",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TimeClock_UserId",
                table: "TimeClock",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeClock_Users_UserId",
                table: "TimeClock",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeClock_Users_UserId",
                table: "TimeClock");

            migrationBuilder.DropIndex(
                name: "IX_TimeClock_UserId",
                table: "TimeClock");

            migrationBuilder.DropColumn(
                name: "Office",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsClockIn",
                table: "TimeClock");

            migrationBuilder.DropColumn(
                name: "Office",
                table: "Payrolls");

            migrationBuilder.AddColumn<int>(
                name: "TimeClockIdTimeclock",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TimeOffset",
                table: "TimeClock",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Users_TimeClockIdTimeclock",
                table: "Users",
                column: "TimeClockIdTimeclock");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TimeClock_TimeClockIdTimeclock",
                table: "Users",
                column: "TimeClockIdTimeclock",
                principalTable: "TimeClock",
                principalColumn: "IdTimeclock");
        }
    }
}
