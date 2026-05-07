using System.Text.Json;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;
using StackExchange.Redis;

namespace ProductMongoRedisGraphQlSerilogReact.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;

    public CacheService(
        IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(value);

        await _database.StringSetAsync(
            key,
            json,
            expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}