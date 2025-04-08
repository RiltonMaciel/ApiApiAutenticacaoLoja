using ApiAutenticacao.DTOs.Pedido;

public interface IPedidoService
{
    Task<List<PedidoDTO>> ObterPedidosAsync(string userId);
}
