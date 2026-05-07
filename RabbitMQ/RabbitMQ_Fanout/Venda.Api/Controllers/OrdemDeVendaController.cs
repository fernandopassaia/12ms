using Microsoft.AspNetCore.Mvc;
using Venda.Api.Models;
using Venda.Api.Services;

namespace Venda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdemDeVendaController : ControllerBase
{
    private readonly RabbitMqService _rabbitMqService;

    public OrdemDeVendaController(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    /// <summary>
    /// Recebe uma venda e publica no RabbitMQ
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CriarVenda([FromBody] OrdemDeVenda venda)
    {
        if (venda is null)
            return BadRequest("Venda inválida");

        if (venda.Itens == null || !venda.Itens.Any())
            return BadRequest("Venda precisa ter itens");

        await _rabbitMqService.PublicarVendaAsync(venda);

        return Ok(new
        {
            mensagem = "Venda enviada para processamento assíncrono",
            vendaId = venda.Id
        });
    }
}