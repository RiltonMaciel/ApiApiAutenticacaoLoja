using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAutenticacao.Migrations
{
    /// <inheritdoc />
    public partial class AddCarrinhoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItens_UserId_ProdutoId",
                table: "CarrinhoItens",
                columns: new[] { "UserId", "ProdutoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItens");
        }
    }
}
