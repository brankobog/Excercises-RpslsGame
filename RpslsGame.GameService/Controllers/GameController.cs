using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Values;

namespace RpslsGame.GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController (ILogger<GameController> logger) : ControllerBase
{
    private static readonly string[] Choices =
        ["Rock", "Paper", "Scissors", "Lizard", "Spock"];

    [HttpGet()]
    [Route("choice")]
    public ChoiceDto GetChoice()
    {
        logger.LogInformation("GET Choice called");
        var choice = Choice.CreateChoice(Random.Shared.Next(0, 5));

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
    public IActionResult PostPlay([FromBody] int playerChoice)
    {
        logger.LogInformation("POST Play called");

        return Ok();
    }
}

public record ChoiceDto(int Id, string Name);
public record PlayerChoiceDto(int Player);
public record PlayResultDto(string Results, int Player, int Computer);