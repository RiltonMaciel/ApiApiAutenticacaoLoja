using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiAutenticacao.Services.Carrinho;
using ApiAutenticacao.Dtos.Carrinho;
using ApiAutenticacao.DTOs.Carrinho;

namespace ApiAutenticacao.Controllers.Carrinho
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterCarrinho()
        {
            var userId = User.Identity!.Name!;
            var carrinho = await _carrinhoService.ObterCarrinhoAsync(userId);
            return Ok(carrinho);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(CarrinhoAddDTO dto)
        {
            var userId = User.Identity!.Name!;
            await _carrinhoService.AdicionarAoCarrinhoAsync(userId, dto);
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult> Remover(int itemId)
        {
            var userId = User.Identity!.Name!;
            await _carrinhoService.RemoverDoCarrinhoAsync(userId, itemId);
            return NoContent();
        }
        [HttpPost("finalizar-compra")]
public async Task<ActionResult> FinalizarCompra()
{
    var userId = User.Identity.Name!;
    await _carrinhoService.FinalizarCompraAsync(userId);
    return Ok(new { mensagem = "Compra finalizada com sucesso!" });
}

[HttpGet("total")]
public async Task<ActionResult<decimal>> ObterTotal()
{
    var userId = User.Identity.Name!;
    var total = await _carrinhoService.CalcularTotalAsync(userId);
    return Ok(total);
}





        [HttpPut]
public async Task<ActionResult> AtualizarQuantidade(CarrinhoUpdateDTO dto)
{
    var userId = User.Identity.Name!;
    await _carrinhoService.AtualizarQuantidadeAsync(userId, dto);
    return NoContent();
}


[HttpDelete("limpar")]
public async Task<IActionResult> LimparCarrinho()
{
    var userId = User.Identity.Name!;
    await _carrinhoService.LimparCarrinhoAsync(userId);
    return NoContent();
}

    }
}

