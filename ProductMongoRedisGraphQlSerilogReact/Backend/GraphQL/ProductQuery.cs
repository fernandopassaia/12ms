using ProductMongoRedisGraphQlSerilogReact.Models;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;

namespace ProductMongoRedisGraphQlSerilogReact.GraphQL;

public class ProductQuery
{
    public async Task<List<Product>> GetProducts(
        [Service] IProductService service)
    {
        return await service.GetAllAsync();
    }
}