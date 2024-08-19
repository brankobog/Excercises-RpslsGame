using RpslsGame.GameService.Choices;

namespace RplsGame.GameService.Tests.Unit.Choices;

public class ChoiceTests
{
    [Fact]
    public void CreateChoice_ShouldReturnCorrectChoice()
    {
        // Arrange
        int rockId = 0;
        int paperId = 1;
        int scissorsId = 2;
        int lizardId = 3;
        int spockId = 4;

        // Act
        var rock = Choice.CreateChoice(rockId);
        var paper = Choice.CreateChoice(paperId);
        var scissors = Choice.CreateChoice(scissorsId);
        var lizard = Choice.CreateChoice(lizardId);
        var spock = Choice.CreateChoice(spockId);

        // Assert
        Assert.Equal("Rock", rock.Name);
        Assert.Equal("Paper", paper.Name);
        Assert.Equal("Scissors", scissors.Name);
        Assert.Equal("Lizard", lizard.Name);
        Assert.Equal("Spock", spock.Name);
    }

    [Fact]
    public void ListChoices_ShouldReturnAllChoices()
    {
        // Act
        var choices = Choice.ListChoices();

        // Assert
        Assert.Contains(Choice.Rock, choices);
        Assert.Contains(Choice.Paper, choices);
        Assert.Contains(Choice.Scissors, choices);
        Assert.Contains(Choice.Lizard, choices);
        Assert.Contains(Choice.Spock, choices);
    }

    [Theory]
    [InlineData(0, 2, true)] // Rock beats Scissors
    [InlineData(0, 3, true)] // Rock beats Lizard
    [InlineData(1, 0, true)] // Paper beats Rock
    [InlineData(1, 4, true)] // Paper beats Spock
    [InlineData(2, 1, true)] // Scissors beats Paper
    [InlineData(2, 3, true)] // Scissors beats Lizard
    [InlineData(3, 1, true)] // Lizard beats Paper
    [InlineData(3, 4, true)] // Lizard beats Spock
    [InlineData(4, 0, true)] // Spock beats Rock
    [InlineData(4, 2, true)] // Spock beats Scissors
    [InlineData(0, 1, false)] // Rock does not beat Paper
    [InlineData(1, 2, false)] // Paper does not beat Scissors
    [InlineData(2, 0, false)] // Scissors does not beat Rock
    [InlineData(3, 0, false)] // Lizard does not beat Rock
    [InlineData(4, 1, false)] // Spock does not beat Paper
    public void Beats_ShouldReturnCorrectResult(int choiceId, int opponentId, bool expected)
    {
        // Arrange
        var choice = Choice.CreateChoice(choiceId);
        var opponent = Choice.CreateChoice(opponentId);

        // Act
        var result = choice.Beats(opponent);

        // Assert
        Assert.Equal(expected, result);
    }
}
