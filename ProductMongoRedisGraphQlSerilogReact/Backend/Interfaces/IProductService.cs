using ProductMongoRedisGraphQlSerilogReact.Models;

namespace ProductMongoRedisGraphQlSerilogReact.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();

    Task CreateAsync(Product Product);
}