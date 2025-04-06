using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacao.DTOs.Produto
{
    public class ProdutoCreateDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Preco { get; set; }

        [Required]
        public string Categoria { get; set; } = string.Empty;
    }
}
