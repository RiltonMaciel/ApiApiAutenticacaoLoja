using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult> ObterPedidos()
    {
        var userId = User.Identity!.Name!;
        var pedidos = await _pedidoService.ObterPedidosAsync(userId);
        return Ok(pedidos);
    }
}
