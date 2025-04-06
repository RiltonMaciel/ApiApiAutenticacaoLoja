using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacao.Models
{
    /// <summary>
    /// Representa um produto da loja.
    /// </summary>
    public class Produto
    {
        [Key] // Identificador único (chave primária no banco)
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public string Categoria { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
