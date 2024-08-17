using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Games;

namespace RpslsGame.GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController (
    ILogger<GameController> logger,
    IRandomChoiceFactory randomChoiceFactory) : ControllerBase
{
    [HttpGet()]
    [Route("choice")]
    public ChoiceDto GetChoice()
    {
        logger.LogInformation("GET Choice called");
        var choice = randomChoiceFactory.CreateRandomChoice();

        logger.LogInformation("Choice: {choice} returned.", choice);
        return new ChoiceDto(choice.Id, choice.Name);
    }

    [HttpGet()]
    [Route("choices")]
    public IEnumerable<ChoiceDto> GetChoices()
    {
        logger.LogInformation("GET Choices called");
        var choices = Choice.ListChoices();

        return choices.Select(c => new ChoiceDto(c.Id, c.Name));
    }

    [HttpPost()]
    [Route("play")]
    public PlayResultDto PostPlay([FromBody] PlayerChoiceDto body)
    {
        logger.LogInformation("POST Play called");
        
        var playerChoice = Choice.CreateChoice(body.Player);
        logger.LogInformation("Player choice: {playerChoice}", playerChoice);
        
        var computerChoice = randomChoiceFactory.CreateRandomChoice();
        logger.LogInformation("Computer choice: {computerChoice}", computerChoice);

        var result = new Game(playerChoice, computerChoice).Result;
        logger.LogInformation("GameResult: {result}", result.ToString());

        return new PlayResultDto(result.ToString(), playerChoice.Id, computerChoice.Id);
    }
}

public record ChoiceDto(int Id, string Name);
public record PlayerChoiceDto(int Player);
public record PlayResultDto(string Results, int Player, int Computer);