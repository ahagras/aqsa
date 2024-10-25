using StackExchange.Redis;

namespace Aqsa.Domain.Common.Services.Cache;

public interface IRedisCacheService
{
    // String operations
    Task SetStringAsync(string key, string value, TimeSpan? expiration = null);
    Task<string> GetStringAsync(string key);

    // Set operations
    Task AddToSetAsync(string key, string value);
    Task<HashSet<string>> GetSetAsync(string key);

    // List operations
    Task AddToListAsync(string key, string value);
    Task<List<string>> GetListAsync(string key);

    // Hash operations
    Task SetHashFieldAsync(string key, string field, string value);
    Task<string> GetHashFieldAsync(string key, string field);

    // Sorted Set operations
    Task AddToSortedSetAsync(string key, string value, double score);
    Task<List<string>> GetSortedSetAsync(string key);

    // Pub/Sub operations
    void Subscribe(string channel, Action<RedisChannel, RedisValue> handler);
    void Publish(string channel, string message);

    // Key operations
    Task<bool> KeyExistsAsync(string key);
    Task RemoveKeyAsync(string key);
}
