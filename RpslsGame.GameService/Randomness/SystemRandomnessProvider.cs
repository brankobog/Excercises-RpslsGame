namespace RpslsGame.GameService.Randomness;

public class SystemRandomnessProvider : IRandomnessProvider
{
    public int Next(int minValue, int maxValue)
    {
        return Random.Shared.Next(minValue, maxValue);
    }

    public Task<int> NextAsync(int minValue, int maxValue)
    {
        return Task.FromResult(Next(minValue, maxValue));
    }
}
