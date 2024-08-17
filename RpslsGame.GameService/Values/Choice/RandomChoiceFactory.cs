namespace RpslsGame.GameService.Values.Choice;

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

public interface IRandomnessProvider
{
    int Next(int minValue, int maxValue);
}

public class SystemRandomnessProvider : IRandomnessProvider
{
    public int Next(int minValue, int maxValue)
    {
        return Random.Shared.Next(minValue, maxValue);
    }
}
