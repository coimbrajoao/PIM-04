using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class CorrigindoFolha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary",
                table: "Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "levelAcesse",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "levelAcesse",
                table: "Users");
        }
    }
}
