using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Games;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RpslsGame.GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(
    ILogger<GameController> logger,
    IRandomChoiceFactory randomChoiceFactory) : ControllerBase
{
    [HttpGet()]
    [Route("choice")]
    [SwaggerOperation(
        Summary = "Get a randomly generated choice",
        Description = "Returns a randomly generated choice out of all the possible choices."
    )]
    [SwaggerResponse(200, "A randomly generated choice", typeof(ChoiceDto))]
    public ChoiceDto GetChoice()
    {
        logger.LogInformation("GET Choice called");
        var choice = randomChoiceFactory.CreateRandomChoice();

        logger.LogInformation("Choice: {choice} returned.", choice);
        return new ChoiceDto(choice.Id, choice.Name);
    }

    [HttpGet()]
    [Route("choices")]
    [SwaggerOperation(
        Summary = "Get all the choices that are usable for the UI",
        Description = "Returns a list of all allowed choices."
    )]
    [SwaggerResponse(200, "A list of all allowed choices", typeof(IEnumerable<ChoiceDto>))]
    public IEnumerable<ChoiceDto> GetChoices()
    {
        logger.LogInformation("GET Choices called");
        var choices = Choice.ListChoices();

        return choices.Select(c => new ChoiceDto(c.Id, c.Name));
    }

    [HttpPost()]
    [Route("play")]
    [SwaggerOperation(
        Summary = "Play a round against a computer opponent",
        Description = "Plays a round against a computer opponent and returns the result."
    )]
    [SwaggerResponse(200, "Result of the round played versus a computer opponent", typeof(PlayResultDto))]
    [SwaggerResponse(400, "Choice validation error")]
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
public record PlayResultDto(string Results, int Player, int Computer);
public record PlayerChoiceDto
{
    [Required]
    [Range(0, 4, ErrorMessage = "Choice is not allowed, choice id is out of bounds.")]
    public int Player { get; init; }
}