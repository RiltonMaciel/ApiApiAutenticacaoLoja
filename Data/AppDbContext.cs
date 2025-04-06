using Microsoft.EntityFrameworkCore;
using ApiAutenticacao.Models;
using ApiAutenticacao.Models.Carrinho; // Importa o modelo de item do carrinho

namespace ApiAutenticacao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔹 Tabela de usuários (já existente)
        public DbSet<User> Users { get; set; }

        // 🔹 Tabela de produtos (já existente)
        public DbSet<Produto> Produtos { get; set; }    

        // ✅ Tabela que representa os itens adicionados ao carrinho de cada usuário
        // Cada item representa 1 produto no carrinho de um usuário específico
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }

        public DbSet<CarrinhoItem> CarrinhoItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 💰 Configura a precisão do campo "Preco" da entidade Produto
            // Define que o preço pode ter até 18 dígitos, sendo 2 deles decimais
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            // 🚫 (Opcional) Garante que o mesmo usuário não adicione o mesmo produto mais de uma vez ao carrinho
            // Se quiser permitir duplicações, comente ou remova este trecho
            modelBuilder.Entity<CarrinhoItem>()
                .HasIndex(ci => new { ci.UserId, ci.ProdutoId })
                .IsUnique(); // Cria um índice único baseado em UserId + ProdutoId
        }
    }
}
