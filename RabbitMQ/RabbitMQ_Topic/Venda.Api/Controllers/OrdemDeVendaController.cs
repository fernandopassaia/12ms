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
        await _rabbitMqService.PublicarEventoAsync(
        venda,
        "venda.criada"
    );

        return Ok(new
        {
            mensagem = "Venda criada",
            vendaId = venda.Id
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirVenda(Guid id)
    {
        var venda = new OrdemDeVenda
        {
            Id = id,
            NomeCliente = "Fernando"
        };

        await _rabbitMqService.PublicarEventoAsync(
            venda,
            "venda.excluida"
        );

        return Ok(new
        {
            mensagem = "Venda excluída",
            vendaId = id
        });
    }
}