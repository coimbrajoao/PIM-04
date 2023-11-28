using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "CPF",
            table: "Users",
            type: "varchar(14)",
            maxLength: 14,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(11)",
            oldMaxLength: 11)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
