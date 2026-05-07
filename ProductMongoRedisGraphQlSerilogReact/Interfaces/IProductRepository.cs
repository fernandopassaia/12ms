using ProductMongoRedisGraphQlSerilogReact.Models;

namespace ProductMongoRedisGraphQlSerilogReact.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task CreateAsync(Product Product);
}