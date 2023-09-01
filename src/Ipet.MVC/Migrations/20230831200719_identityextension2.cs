using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipet.MVC.Migrations
{
    public partial class identityextension2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "AspNetUsers",
                newName: "Documento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "AspNetUsers",
                newName: "Cnpj");
        }
    }
}
