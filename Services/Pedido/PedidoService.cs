
using ApiAutenticacao.Data;
using ApiAutenticacao.DTOs.Pedido;
using ApiAutenticacao.Models;
using Microsoft.EntityFrameworkCore;

public class PedidoService : IPedidoService
{
    private readonly AppDbContext _context;

    public PedidoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PedidoDTO>> ObterPedidosAsync(string userId)
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Itens)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();

        return pedidos.Select(p => new PedidoDTO
        {
            Id = p.Id,
            DataPedido = p.DataPedido,
            Itens = p.Itens.Select(i => new PedidoItemDTO
            {
                ProdutoId = i.ProdutoId,
                NomeProduto = i.NomeProduto,
                Quantidade = i.Quantidade
            }).ToList()
        }).ToList();
    }
}
