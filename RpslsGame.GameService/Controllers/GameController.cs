using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Games;
using RpslsGame.GameService.Redis;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RpslsGame.GameService.Controllers;

[ApiController]
public class GameController(
    IRandomChoiceFactory randomChoiceFactory,
    ILeaderboardService leaderboard,
    ILogger<GameController> logger) : ControllerBase
{
    [HttpPost("play")]
    [SwaggerOperation(
        Summary = "Play a round against a computer opponent",
        Description = "Plays a round against a computer opponent and returns the result."
    )]
    [SwaggerResponse(200, "Result of the round played versus a computer opponent", typeof(PlayResultDto))]
    [SwaggerResponse(400, "Choice validation error")]
    public async Task<PlayResultDto> PostPlay([FromBody] PlayerChoiceDto body)
    {
        logger.LogInformation("POST Play called");

        string? userId = HttpContext.Session.GetString(PlayerController.SessionKey);
        logger.LogInformation("Player id :{userId}", userId);

        var playerChoice = Choice.CreateChoice(body.Player);
        logger.LogInformation("Player choice: {playerChoice}", playerChoice.Name);
        
        var computerChoice = await randomChoiceFactory.CreateRandomChoiceAsync();
        logger.LogInformation("Computer choice: {computerChoice}", computerChoice.Name);

        var result = new Game(playerChoice, computerChoice).Result;
        logger.LogInformation("GameResult: {result}", result.ToString());

        if (userId != null && result == GameResult.Win)
        {
            await leaderboard.IncrementScoreAsync(userId.ToString());
        }

        return new PlayResultDto(result.ToString(), playerChoice.Id, computerChoice.Id);
    }
}

public record PlayerChoiceDto
{
    [Required]
    [Range(0, 4, ErrorMessage = "Choice is not allowed, choice id is out of bounds.")]
    public int Player { get; init; }
}
public record PlayResultDto(string Results, int Player, int Computer);