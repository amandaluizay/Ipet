using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipet.Data.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtag_Produtos_produtoId",
                table: "Hashtag");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtag_Servicos_servicoId",
                table: "Hashtag");

            migrationBuilder.DropIndex(
                name: "IX_Hashtag_produtoId",
                table: "Hashtag");

            migrationBuilder.DropIndex(
                name: "IX_Hashtag_servicoId",
                table: "Hashtag");

            migrationBuilder.DropColumn(
                name: "produtoId",
                table: "Hashtag");

            migrationBuilder.DropColumn(
                name: "servicoId",
                table: "Hashtag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "produtoId",
                table: "Hashtag",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "servicoId",
                table: "Hashtag",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Hashtag_produtoId",
                table: "Hashtag",
                column: "produtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashtag_servicoId",
                table: "Hashtag",
                column: "servicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtag_Produtos_produtoId",
                table: "Hashtag",
                column: "produtoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtag_Servicos_servicoId",
                table: "Hashtag",
                column: "servicoId",
                principalTable: "Servicos",
                principalColumn: "Id");
        }
    }
}
