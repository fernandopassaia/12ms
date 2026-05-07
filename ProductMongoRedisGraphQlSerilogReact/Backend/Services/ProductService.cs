using ProductMongoRedisGraphQlSerilogReact.Models;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using Serilog;

namespace ProductMongoRedisGraphQlSerilogReact.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cacheService;

    public ProductService(
        IProductRepository repository,
        ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        const string cacheKey = "products";

        Log.Information("Checking for Redis cache...");

        var cachedProducts =
            await _cacheService.GetAsync<List<Product>>(cacheKey);

        if (cachedProducts is not null)
        {
            Log.Information("Products taken for Redis Cache...");

            return cachedProducts;
        }

        Log.Information("Products NOT FOUND on Redis Cache...");

        var products =
            await _repository.GetAllAsync();

        Log.Information("Saving products in Redis Cache...");

        await _cacheService.SetAsync(
            cacheKey,
            products,
            TimeSpan.FromMinutes(5));

        return products;
    }

    public async Task CreateAsync(Product Product)
    {
        Product.Id = Guid.NewGuid();

        Product.DataCadastro = DateTime.UtcNow;

        await _repository.CreateAsync(Product);

        await _cacheService.RemoveAsync("products");

        Log.Information("Products cache invalidated");
    }
}