namespace RpslsGame.GameService.Redis;

public interface ILeaderboardService
{
    Task<LeaderboardScore?> GetScoreAsync(string key);
    Task<IEnumerable<LeaderboardScore>> GetRange(int start, int end);
    Task IncrementScoreAsync(string key);
    Task ClearAsync();
}
