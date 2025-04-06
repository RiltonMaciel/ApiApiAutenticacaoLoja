using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAutenticacao.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhoFinalizarCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrinhoItens",
                table: "CarrinhoItens");

            migrationBuilder.RenameTable(
                name: "CarrinhoItens",
                newName: "CarrinhoItem");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItens_UserId_ProdutoId",
                table: "CarrinhoItem",
                newName: "IX_CarrinhoItem_UserId_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrinhoItem",
                table: "CarrinhoItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrinhoItem",
                table: "CarrinhoItem");

            migrationBuilder.RenameTable(
                name: "CarrinhoItem",
                newName: "CarrinhoItens");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoItem_UserId_ProdutoId",
                table: "CarrinhoItens",
                newName: "IX_CarrinhoItens_UserId_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrinhoItens",
                table: "CarrinhoItens",
                column: "Id");
        }
    }
}
