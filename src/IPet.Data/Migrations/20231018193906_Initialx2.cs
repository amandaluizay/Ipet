using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipet.Data.Migrations
{
    public partial class Initialx2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PerfilPet",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PerfilPet");
        }
    }
}
