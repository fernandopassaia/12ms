
using ProductMongoRedisGraphQlSerilogReact.Models;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using ProductMongoRedisGraphQlSerilogReact.GraphQL.Inputs;

namespace ProductMongoRedisGraphQlSerilogReact.GraphQL;

public class ProductMutation
{
    public async Task<bool> CreateProduct(
        CreateProductInput input,
        [Service] IProductService service)
    {
        var product = new Product
        {
            Descricao = input.Descricao,
            Valor = input.Valor,
            Estoque = input.Estoque
        };

        await service.CreateAsync(product);

        return true;
    }
}