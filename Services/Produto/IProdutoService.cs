using ApiAutenticacao.DTOs.Produto;

namespace ApiAutenticacao.Services.Produto
{
    public interface IProdutoService
    {
        Task<List<ProdutoDTO>> ObterTodosAsync();
        Task<ProdutoDTO?> ObterPorIdAsync(int id);
        Task<ProdutoDTO> CriarAsync(ProdutoCreateDTO dto);
        Task<bool> AtualizarAsync(int id, ProdutoUpdateDTO dto);
        Task<bool> DeletarAsync(int id);
    }
}
