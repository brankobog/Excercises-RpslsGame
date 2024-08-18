using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RpslsGame.GameService.Controllers;

[ApiController]
public class PlayerController(
    ILogger<PlayerController> logger) : ControllerBase
{
    public static readonly string SessionKey = "playerId";
    private const string AnonName = "Anonymous";

    [HttpPost("name")]
    [SwaggerOperation(
        Summary = "Set the current players name"
    )]
    [SwaggerResponse(200, "A randomly generated choice", typeof(ChoiceDto))]
    public PlayerIdDto PostPlayerName(string name)
    {
        logger.LogInformation("POST Player Name called");

        int id;
        id = Random.Shared.Next(1000000);
        
        name = !string.IsNullOrEmpty(name) ? 
            $"{name.Trim()}_{id}" : 
            $"{AnonName}_{id}";
        
        HttpContext.Session.SetString(SessionKey, name);

        logger.LogInformation("Saved Player: {name}", name);

        return new PlayerIdDto(id, name);
    }

}

public record PlayerIdDto(int Id, string Name);
