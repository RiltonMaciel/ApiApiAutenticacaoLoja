namespace ApiAutenticacao.DTOs.Pedido;

public class PedidoItemDTO
{
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}
