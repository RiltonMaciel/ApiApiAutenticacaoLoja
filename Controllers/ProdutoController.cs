using ApiAutenticacao.DTOs.Produto;
using ApiAutenticacao.Services.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAutenticacao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ Protege os endpoints - só acessa com token válido
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        // 🧠 Injeção de dependência do serviço
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: api/produto
        [HttpGet]
        public async Task<ActionResult<List<ProdutoDTO>>> ObterTodos()
        {
            var produtos = await _produtoService.ObterTodosAsync();
            return Ok(produtos);
        }

        // GET: api/produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> ObterPorId(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        // POST: api/produto
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Criar([FromBody] ProdutoCreateDTO dto)
        {
            var novoProduto = await _produtoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT: api/produto/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] ProdutoUpdateDTO dto)
        {
            var atualizado = await _produtoService.AtualizarAsync(id, dto);
            if (!atualizado) return NotFound();
            return NoContent(); // ✅ Atualizado com sucesso, mas sem conteúdo
        }

        // DELETE: api/produto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var removido = await _produtoService.DeletarAsync(id);
            if (!removido) return NotFound();
            return NoContent(); // ✅ Removido com sucesso
        }
    }
}
