using ApiAutenticacao.Data;
using ApiAutenticacao.Dtos.Carrinho;
using ApiAutenticacao.DTOs.Carrinho;
using ApiAutenticacao.Models.Carrinho;
using Microsoft.EntityFrameworkCore;

namespace ApiAutenticacao.Services.Carrinho
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly AppDbContext _context;

        public CarrinhoService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de itens no carrinho de um usuário.
        /// Busca os dados reais do Produto pelo relacionamento com CarrinhoItem.
        /// </summary>
        public async Task<List<CarrinhoItemDTO>> ObterCarrinhoAsync(string userId)
        {
            var itens = await _context.CarrinhoItens
                .Where(x => x.UserId == userId)
                .Include(x => x.Produto) // Inclui o Produto vinculado
                .ToListAsync();

            // Mapeia cada item para o DTO com nome real do produto
            return itens.Select(item => new CarrinhoItemDTO
            {
                Id = item.Id,
                ProdutoId = item.ProdutoId,
                NomeProduto = item.Produto != null ? item.Produto.Nome : "Produto não encontrado",
                Quantidade = item.Quantidade
            }).ToList();
        }

        /// <summary>
        /// Adiciona um novo item ao carrinho do usuário.
        /// </summary>
        public async Task AdicionarAoCarrinhoAsync(string userId, CarrinhoAddDTO dto)
        {
            var item = new CarrinhoItem
            {
                UserId = userId,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade
            };

            _context.CarrinhoItens.Add(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um item específico do carrinho do usuário.
        /// </summary>
        public async Task RemoverDoCarrinhoAsync(string userId, int itemId)
        {
            var item = await _context.CarrinhoItens
                .FirstOrDefaultAsync(x => x.Id == itemId && x.UserId == userId);

            if (item != null)
            {
                _context.CarrinhoItens.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Calcula o valor total dos itens no carrinho.
        /// </summary>
        public async Task<decimal> CalcularTotalAsync(string userId)
        {
            var itens = await _context.CarrinhoItens
                .Include(x => x.Produto)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return itens.Sum(item => (item.Produto?.Preco ?? 0) * item.Quantidade);
        }

        /// <summary>
        /// Atualiza a quantidade de um item no carrinho.
        /// </summary>
        public async Task AtualizarQuantidadeAsync(string userId, CarrinhoUpdateDTO dto)
        {
            var item = await _context.CarrinhoItens
                .FirstOrDefaultAsync(x => x.Id == dto.ItemId && x.UserId == userId);

            if (item == null)
                throw new Exception("Item não encontrado no carrinho.");

            item.Quantidade = dto.Quantidade;

            await _context.SaveChangesAsync();
        }

      /// <summary>
/// Finaliza a compra do carrinho:
/// - Cria um novo pedido com os itens do carrinho.
/// - Associa cada item ao pedido como PedidoItem.
/// - Remove os itens do carrinho após finalizar.
/// </summary>
public async Task FinalizarCompraAsync(string userId)
{
    var carrinho = await _context.CarrinhoItens
        .Include(ci => ci.Produto) // Carrega dados do produto
        .Where(ci => ci.UserId == userId)
        .ToListAsync();

    if (!carrinho.Any())
        throw new Exception("Carrinho está vazio. Adicione produtos antes de finalizar.");

    // Criação do novo Pedido
    var pedido = new Models.Pedido.Pedido
    {
        UserId = userId,
        DataPedido = DateTime.UtcNow,
        Itens = carrinho.Select(ci => new Models.Pedido.PedidoItem
        {
            ProdutoId = ci.ProdutoId,
            NomeProduto = ci.Produto?.Nome ?? "Produto não encontrado",
            Quantidade = ci.Quantidade,
            PrecoUnitario = ci.Produto?.Preco ?? 0
        }).ToList()
    };

    await _context.Pedidos.AddAsync(pedido);

    // Remove os itens do carrinho
    _context.CarrinhoItens.RemoveRange(carrinho);

    // Salva tudo no banco
    await _context.SaveChangesAsync();
}


        /// <summary>
        /// Remove todos os itens do carrinho.
        /// </summary>
        public async Task LimparCarrinhoAsync(string userId)
        {
            var itens = await _context.CarrinhoItens
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.CarrinhoItens.RemoveRange(itens);
            await _context.SaveChangesAsync();
        }
    }
}
