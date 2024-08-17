using RpslsGame.GameService.Randomness;

namespace RpslsGame.GameService.Choices;

public interface IRandomChoiceFactory
{
    Choice CreateRandomChoice();
}

public class RandomChoiceFactory(IRandomnessProvider randomGenerator) : IRandomChoiceFactory
{
    public Choice CreateRandomChoice()
    {
        return Choice.CreateChoice(randomGenerator.Next(0, Choice.ListChoices().Count()));
    }
}
