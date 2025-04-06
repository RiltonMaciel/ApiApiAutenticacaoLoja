namespace ApiAutenticacao.Models.Carrinho
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
