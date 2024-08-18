using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Redis;
using Swashbuckle.AspNetCore.Annotations;

namespace RpslsGame.GameService.Controllers;

[ApiController]
public class LeaderboardController(
    ILeaderboardService leaderboard,
    ILogger<LeaderboardController> logger) : ControllerBase
{
    [HttpGet("leaderboard")]
    [SwaggerOperation(
        Summary = "Get leaderboard",
        Description = "Get the top 5 players on the leaderboard"
    )]
    [SwaggerResponse(200, "The leaderboard", typeof(ChoiceDto))]
    public async Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync()
    {
        logger.LogInformation("GET Leaderboard called");
        var scores = await leaderboard.GetRange(0, 10);

        return scores.Select(score =>
            new LeaderboardEntryDto(score.Name, score.Score, score.Rank)
        );
    }

    [HttpDelete("leaderboard")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    [SwaggerOperation(
        Summary = "Clear leaderboard",
        Description = "Clear all scores from the leaderboard"
    )]
    [SwaggerResponse(200, "The leaderboard is cleared succesfuly", typeof(ChoiceDto))]
    [SwaggerResponse(401, "Unauthorized")]
    public async Task DeleteLeaderboardAsync()
    {
        logger.LogInformation("DELETE Leaderboard called");
        await leaderboard.ClearAsync();
    }
}

public record LeaderboardEntryDto(string Name, int Score, int Rank);