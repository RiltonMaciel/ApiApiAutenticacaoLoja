namespace ApiAutenticacao.Dtos.Carrinho
{
    public class CarrinhoItemDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
