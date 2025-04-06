namespace ApiAutenticacao.DTOs.Carrinho
{
    public class CarrinhoUpdateDTO
    {
        public int ItemId { get; set; } // ID do item no carrinho
        public int Quantidade { get; set; } // Nova quantidade
    }
}
