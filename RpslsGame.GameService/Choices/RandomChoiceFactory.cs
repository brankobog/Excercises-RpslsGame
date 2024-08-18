using RpslsGame.GameService.Randomness;

namespace RpslsGame.GameService.Choices;

public interface IRandomChoiceFactory
{
    Task<Choice> CreateRandomChoiceAsync();
    Choice CreateRandomChoice();
}

public class RandomChoiceFactory(IRandomnessProvider randomGenerator) : IRandomChoiceFactory
{
    public async Task<Choice> CreateRandomChoiceAsync()
    {
        return Choice.CreateChoice(await randomGenerator.NextAsync(0, Choice.ListChoices().Count()));
    }
    public Choice CreateRandomChoice()
    {
        return Choice.CreateChoice(randomGenerator.Next(0, Choice.ListChoices().Count()));
    }
}
