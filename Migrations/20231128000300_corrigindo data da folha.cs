using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class corrigindodatadafolha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date_of_competence",
                table: "Payrolls",
                newName: "Date_of_competence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date_of_competence",
                table: "Payrolls",
                newName: "date_of_competence");
        }
    }
}
