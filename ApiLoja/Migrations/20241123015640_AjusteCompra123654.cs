using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoja.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCompra123654 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClienteId1",
                table: "Compras",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProdutoId1",
                table: "Compras",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ClienteId1",
                table: "Compras",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProdutoId1",
                table: "Compras",
                column: "ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Clientes_ClienteId1",
                table: "Compras",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Produtos_ProdutoId1",
                table: "Compras",
                column: "ProdutoId1",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Clientes_ClienteId1",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Produtos_ProdutoId1",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_ClienteId1",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_ProdutoId1",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "Compras");
        }
    }
}
