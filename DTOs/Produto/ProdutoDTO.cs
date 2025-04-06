namespace ApiAutenticacao.DTOs.Produto
{
    public class ProdutoDTO
    {
        public int Id { get; set; }                 // Identificador único
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }  // Data em que o produto foi cadastrado
    }
}
