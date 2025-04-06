using ApiAutenticacao.Data;
using ApiAutenticacao.DTOs.Produto;
using ApiAutenticacao.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAutenticacao.Services.Produto
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Método para retornar todos os produtos cadastrados
        public async Task<List<ProdutoDTO>> ObterTodosAsync()
        {
            // Busca todos os produtos do banco de dados
            var produtos = await _context.Produtos.ToListAsync();

            // Mapeia os dados do modelo para o DTO
            return produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Categoria = p.Categoria,
                DataCadastro = p.DataCadastro
            }).ToList();
        }

        // ✅ Retorna apenas um produto pelo ID
        public async Task<ProdutoDTO?> ObterPorIdAsync(int id)
        {
            var p = await _context.Produtos.FindAsync(id);
            if (p == null) return null;

            return new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Categoria = p.Categoria,
                DataCadastro = p.DataCadastro
            };
        }

        // ✅ Criação de um novo produto
        public async Task<ProdutoDTO> CriarAsync(ProdutoCreateDTO dto)
        {
            var novoProduto = new Models.Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                Categoria = dto.Categoria,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(novoProduto);
            await _context.SaveChangesAsync(); // Salva no banco

            return new ProdutoDTO
            {
                Id = novoProduto.Id,
                Nome = novoProduto.Nome,
                Descricao = novoProduto.Descricao,
                Preco = novoProduto.Preco,
                Categoria = novoProduto.Categoria,
                DataCadastro = novoProduto.DataCadastro
            };

            Console.WriteLine("Produto criado com ID: " + novoProduto.Id);

        }

        // ✅ Atualização dos dados de um produto existente
        public async Task<bool> AtualizarAsync(int id, ProdutoUpdateDTO dto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.Preco = dto.Preco;
            produto.Categoria = dto.Categoria;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Exclusão de um produto
        public async Task<bool> DeletarAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
