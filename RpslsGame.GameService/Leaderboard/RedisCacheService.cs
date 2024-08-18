using StackExchange.Redis;
using System.Text.Json;

namespace RpslsGame.GameService.Redis;

public interface ILeaderboardService
{
    Task<LeaderboardScore?> GetScoreAsync(string key);
    Task<IEnumerable<LeaderboardScore>> GetRange(int start, int end, bool isDesc);
    Task IncrementScoreAsync(string key);
    Task ClearAsync();
}

public record LeaderboardScore(string Name, int Score);

public class RedisLeaderboardService(IConnectionMultiplexer redis) : ILeaderboardService
{
    private readonly IDatabase _database = redis.GetDatabase();
    public async Task<LeaderboardScore?> GetScoreAsync(string key)
    {
        var item = await _database.HashGetAllAsync(key);
        if (item is null || item.Length == 0)
        {
            return null;
        }
        return new(item[0].ToString(), 1);
    }

    public async Task IncrementScoreAsync(string key)
    {
        await _database.SortedSetIncrementAsync("leaderboard", key, 1.0);
    }

    public Task<IEnumerable<LeaderboardScore>> GetRange(int start, int end, bool isDesc)
    {
        throw new NotImplementedException();
    }

    public async Task ClearAsync()
    {
        var endpoints = redis.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = redis.GetServer(endpoint);
            await server.FlushDatabaseAsync();
        }
    }
}