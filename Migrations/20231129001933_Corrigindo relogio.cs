using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class Corrigindorelogio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClockIn",
                table: "TimeClock");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "TimeClock",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TimeClock",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "TimeClock",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TimeClock");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "TimeClock");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "TimeClock",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AddColumn<bool>(
                name: "IsClockIn",
                table: "TimeClock",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
