using Microsoft.AspNetCore.Mvc;

namespace RpslsGame.GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController (ILogger<GameController> logger) : ControllerBase
{
    private static readonly string[] Choices =
        ["Rock", "Paper", "Scissors", "Lizard", "Spock"];


    [HttpGet()]
    [Route("choice")]
    public string GetChoice()
    {
        logger.LogInformation("GetChoice called");
        var choice = Choices[Random.Shared.Next(0, 5)];

        logger.LogInformation("Choice: {choice} returned.", choice);
        return choice;
    }
}
