using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAutenticacao.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarCarrinhoComProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItem_ProdutoId",
                table: "CarrinhoItem",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_Produtos_ProdutoId",
                table: "CarrinhoItem",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_Produtos_ProdutoId",
                table: "CarrinhoItem");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhoItem_ProdutoId",
                table: "CarrinhoItem");
        }
    }
}
