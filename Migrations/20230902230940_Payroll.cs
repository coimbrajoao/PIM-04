using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Migrations
{
    public partial class Payroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Payrolls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
