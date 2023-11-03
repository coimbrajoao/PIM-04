using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class corrigindojson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Users",
                newName: "registration");

            migrationBuilder.RenameColumn(
                name: "Matricula",
                table: "Users",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Users",
                newName: "Publicplace");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registration",
                table: "Users",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Publicplace",
                table: "Users",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Users",
                newName: "Matricula");
        }
    }
}
