using Microsoft.AspNetCore.Mvc;
using ProductMongoRedisGraphQlSerilogReact.Models;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;

namespace ProductMongoRedisGraphQlSerilogReact.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(
        IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Products = await _service.GetAllAsync();

        return Ok(Products);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        Product Product)
    {
        await _service.CreateAsync(Product);

        return Ok();
    }
}