namespace ApiAutenticacao.DTOs.Pedido;

public class PedidoDTO
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; }
    public List<PedidoItemDTO> Itens { get; set; } = new();
}
