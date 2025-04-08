using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacao.Models.Carrinho
{
    public class CarrinhoItem
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        // Chave estrangeira
        public int ProdutoId { get; set; }

        // Navegação
        public Produto Produto { get; set; } = null!;

        public int Quantidade { get; set; }

        public int Preco { get; set; }  // valor unitário atual
        
    }
}
