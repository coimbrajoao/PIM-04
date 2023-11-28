using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class Corrigindocampodeacesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "levelAcesse",
                table: "Users",
                newName: "LevelAcesse");

            migrationBuilder.AlterColumn<string>(
                name: "LevelAcesse",
                table: "Users",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 1)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LevelAcesse",
                table: "Users",
                newName: "levelAcesse");

            migrationBuilder.AlterColumn<int>(
                name: "levelAcesse",
                table: "Users",
                type: "int",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
