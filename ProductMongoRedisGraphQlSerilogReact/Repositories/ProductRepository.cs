using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductMongoRedisGraphQlSerilogReact.Configurations;
using ProductMongoRedisGraphQlSerilogReact.Models;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using Serilog;

namespace ProductMongoRedisGraphQlSerilogReact.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(
        IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Product>(
            settings.Value.ProductsCollectionName);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        await Task.Delay(3000); //simula demora pra testar cache
        Log.Information("Hitting MongoDB Database do Retrieve Products...");
        return await _collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task CreateAsync(Product Product)
    {
        await _collection.InsertOneAsync(Product);
    }
}