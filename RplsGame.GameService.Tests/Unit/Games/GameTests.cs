using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Games;

namespace RplsGame.GameService.Tests.Unit.Games;

public class GameTests
{
    [Theory]
    [InlineData(0, 0, GameResult.Tie)] // Rock vs Rock
    [InlineData(1, 1, GameResult.Tie)] // Paper vs Paper
    [InlineData(2, 2, GameResult.Tie)] // Scissors vs Scissors
    [InlineData(3, 3, GameResult.Tie)] // Lizard vs Lizard
    [InlineData(4, 4, GameResult.Tie)] // Spock vs Spock
    [InlineData(0, 2, GameResult.Win)] // Rock beats Scissors
    [InlineData(0, 3, GameResult.Win)] // Rock beats Lizard
    [InlineData(1, 0, GameResult.Win)] // Paper beats Rock
    [InlineData(1, 4, GameResult.Win)] // Paper beats Spock
    [InlineData(2, 1, GameResult.Win)] // Scissors beats Paper
    [InlineData(2, 3, GameResult.Win)] // Scissors beats Lizard
    [InlineData(3, 1, GameResult.Win)] // Lizard beats Paper
    [InlineData(3, 4, GameResult.Win)] // Lizard beats Spock
    [InlineData(4, 0, GameResult.Win)] // Spock beats Rock
    [InlineData(4, 2, GameResult.Win)] // Spock beats Scissors
    [InlineData(0, 1, GameResult.Lose)] // Rock loses to Paper
    [InlineData(1, 2, GameResult.Lose)] // Paper loses to Scissors
    [InlineData(2, 0, GameResult.Lose)] // Scissors loses to Rock
    [InlineData(3, 0, GameResult.Lose)] // Lizard loses to Rock
    [InlineData(4, 1, GameResult.Lose)] // Spock loses to Paper
    public void Result_ShouldReturnCorrectGameResult(int playerChoiceId, int opponentChoiceId, GameResult expected)
    {
        // Arrange
        var playerChoice = Choice.CreateChoice(playerChoiceId);
        var opponentChoice = Choice.CreateChoice(opponentChoiceId);
        var game = new Game(playerChoice, opponentChoice);

        // Act
        var result = game.Result;

        // Assert
        Assert.Equal(expected, result);
    }
}
