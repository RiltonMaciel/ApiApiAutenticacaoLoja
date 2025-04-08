using ApiAutenticacao.Dtos.Carrinho;
using ApiAutenticacao.DTOs.Carrinho;

namespace ApiAutenticacao.Services.Carrinho
{
    public interface ICarrinhoService
    {
        Task<List<CarrinhoItemDTO>> ObterCarrinhoAsync(string userId);
        Task AdicionarAoCarrinhoAsync(string userId, CarrinhoAddDTO dto);
        Task RemoverDoCarrinhoAsync(string userId, int itemId);
        Task AtualizarQuantidadeAsync(string userId, CarrinhoUpdateDTO dto);
        Task FinalizarCompraAsync(string userId);
        Task<decimal> CalcularTotalAsync(string userId);
        Task LimparCarrinhoAsync(string userId);




    }
}
