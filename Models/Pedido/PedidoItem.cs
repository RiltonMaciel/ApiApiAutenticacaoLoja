using ApiAutenticacao.Models; // Importa o modelo Produto
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAutenticacao.Models.Pedido
{
    // 🔹 Representa cada item de um pedido finalizado
    public class PedidoItem
    {
        [Key]
        public int Id { get; set; } // Identificador único do item

        // 🔗 Relacionamento com Pedido
        public int PedidoId { get; set; } // Chave estrangeira para o Pedido
        public Pedido Pedido { get; set; } // Navegação para o Pedido pai

        // 🔗 Relacionamento com Produto
        public int ProdutoId { get; set; } // Chave estrangeira para o Produto
        public Produto Produto { get; set; } // Produto que foi comprado

        // 📦 Quantidade comprada deste produto
        public int Quantidade { get; set; }

        // 💰 Preço do produto na hora da compra
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }
        public string NomeProduto { get; internal set; }
    }
}
