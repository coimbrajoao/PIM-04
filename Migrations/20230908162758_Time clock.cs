using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class Timeclock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TimeClockIdTimeclock",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeClock",
                columns: table => new
                {
                    IdTimeclock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimeOffset = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeClock", x => x.IdTimeclock);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TimeClock_TimeClockIdTimeclock",
                table: "Users");

            migrationBuilder.DropTable(
                name: "TimeClock");

            migrationBuilder.DropIndex(
                name: "IX_Users_TimeClockIdTimeclock",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TimeClockIdTimeclock",
                table: "Users");
        }
    }
}
