using Microsoft.EntityFrameworkCore;
using ApiAutenticacao.Models;
using ApiAutenticacao.Models.Carrinho; // Modelos do carrinho
using ApiAutenticacao.Models.Pedido;   // Modelos de pedido

namespace ApiAutenticacao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔹 Tabela de usuários
        public DbSet<User> Users { get; set; }

        // 🔹 Tabela de produtos disponíveis na loja
        public DbSet<Produto> Produtos { get; set; }    

        // ✅ Tabela que representa os itens adicionados ao carrinho de cada usuário
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }

        // ⚠️ Você tem dois DbSet iguais (CarrinhoItens e CarrinhoItems). Pode remover um.
        public DbSet<CarrinhoItem> CarrinhoItems { get; set; }

        // ✅ Tabela de Pedidos (representa a compra finalizada por um usuário)
        public DbSet<Pedido> Pedidos { get; set; }

        // ✅ Tabela dos itens de cada Pedido (ex: um tênis, uma camiseta, etc.)
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 💰 Define precisão para o campo Preco (18 inteiros, 2 decimais)
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            // 🔗 Define relacionamento entre CarrinhoItem e Produto
            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(ci => ci.Produto)
                .WithMany() // ou .WithMany(p => p.CarrinhoItens) se quiser fazer o caminho reverso
                .HasForeignKey(ci => ci.ProdutoId);

            // 🚫 Impede que o mesmo produto seja adicionado mais de uma vez por um mesmo usuário
            modelBuilder.Entity<CarrinhoItem>()
                .HasIndex(ci => new { ci.UserId, ci.ProdutoId })
                .IsUnique();

            // 🔗 Relacionamento entre Pedido e PedidoItem
            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(pi => pi.PedidoId);

            // 🔗 Relacionamento entre PedidoItem e Produto
            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId);
        }
    }
}
