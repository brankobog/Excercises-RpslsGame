using Microsoft.AspNetCore.Mvc;
using RpslsGame.GameService.Choices;
using Swashbuckle.AspNetCore.Annotations;

namespace RpslsGame.GameService.Controllers;

[ApiController]
public class ChoiceController(
    IRandomChoiceFactory randomChoiceFactory,
    ILogger<GameController> logger) : ControllerBase
{
    [HttpGet("choice")]
    [SwaggerOperation(
        Summary = "Get a randomly generated choice",
        Description = "Returns a randomly generated choice out of all the possible choices."
    )]
    [SwaggerResponse(200, "A randomly generated choice", typeof(ChoiceDto))]
    public async Task<ChoiceDto> GetChoiceAsync()
    {
        logger.LogInformation("GET Choice called");
        var choice = await randomChoiceFactory.CreateRandomChoiceAsync();

        logger.LogInformation("Choice: {choice} returned.", choice);
        return new ChoiceDto(choice.Id, choice.Name);
    }

    [HttpGet("choices")]
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
}

public record ChoiceDto(int Id, string Name);
