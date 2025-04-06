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

        public async Task<List<CarrinhoItemDTO>> ObterCarrinhoAsync(string userId)
        {
            return await _context.CarrinhoItens
                .Where(x => x.UserId == userId)
                .Select(x => new CarrinhoItemDTO
                {
                    Id = x.Id,
                    ProdutoId = x.ProdutoId,
                    NomeProduto = x.NomeProduto,
                    Quantidade = x.Quantidade
                })
                .ToListAsync();
        }

        public async Task AdicionarAoCarrinhoAsync(string userId, CarrinhoAddDTO dto)
        {
            var item = new CarrinhoItem
            {
                UserId = userId,
                ProdutoId = dto.ProdutoId,
                NomeProduto = $"Produto {dto.ProdutoId}",
                Quantidade = dto.Quantidade
            };

            _context.CarrinhoItens.Add(item);
            await _context.SaveChangesAsync();
        }

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
        /// Atualiza a quantidade de um item já existente no carrinho.
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

      public async Task FinalizarCompraAsync(string userId)
{
    var itens = await _context.CarrinhoItens
        .Where(c => c.UserId == userId)
        .ToListAsync();

    if (!itens.Any())
        throw new Exception("Carrinho já está vazio.");

    _context.CarrinhoItems.RemoveRange(itens);
    await _context.SaveChangesAsync();
}
    }}
