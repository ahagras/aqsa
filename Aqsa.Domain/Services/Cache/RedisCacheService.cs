using StackExchange.Redis;

namespace Aqsa.Domain.Common.Services.Cache
{
    public class RedisCacheService(IConnectionMultiplexer redis) : IRedisCacheService
    {
        // String operations
        public async Task SetStringAsync(string key, string value, TimeSpan? expiration = null)
        {
            var db = redis.GetDatabase();
            await db.StringSetAsync(key, value, expiration);
        }

        public async Task<string> GetStringAsync(string key)
        {
            var db = redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        // Set operations
        public async Task AddToSetAsync(string key, string value)
        {
            var db = redis.GetDatabase();
            await db.SetAddAsync(key, value);
        }

        public async Task<HashSet<string>> GetSetAsync(string key)
        {
            var db = redis.GetDatabase();
            var values = await db.SetMembersAsync(key);
            return new HashSet<string>(values.ToStringArray());
        }

        // List operations
        public async Task AddToListAsync(string key, string value)
        {
            var db = redis.GetDatabase();
            await db.ListRightPushAsync(key, value);
        }

        public async Task<List<string>> GetListAsync(string key)
        {
            var db = redis.GetDatabase();
            var values = await db.ListRangeAsync(key);
            return values.Select(v => v.ToString()).ToList();
        }

        // Hash operations
        public async Task SetHashFieldAsync(string key, string field, string value)
        {
            var db = redis.GetDatabase();
            await db.HashSetAsync(key, field, value);
        }

        public async Task<string> GetHashFieldAsync(string key, string field)
        {
            var db = redis.GetDatabase();
            return await db.HashGetAsync(key, field);
        }

        // Sorted Set operations
        public async Task AddToSortedSetAsync(string key, string value, double score)
        {
            var db = redis.GetDatabase();
            await db.SortedSetAddAsync(key, value, score);
        }

        public async Task<List<string>> GetSortedSetAsync(string key)
        {
            var db = redis.GetDatabase();
            var values = await db.SortedSetRangeByRankAsync(key);
            return values.Select(v => v.ToString()).ToList();
        }

        // Pub/Sub operations
        public void Subscribe(string channel, Action<RedisChannel, RedisValue> handler)
        {
            var sub = redis.GetSubscriber();
            sub.Subscribe(channel, handler);
        }

        public void Publish(string channel, string message)
        {
            var sub = redis.GetSubscriber();
            sub.Publish(channel, message);
        }

        // Key operations
        public async Task<bool> KeyExistsAsync(string key)
        {
            var db = redis.GetDatabase();
            return await db.KeyExistsAsync(key);
        }

        public async Task RemoveKeyAsync(string key)
        {
            var db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }

}
