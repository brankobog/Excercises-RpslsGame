namespace RpslsGame.GameService.Randomness;

public interface IRandomnessProvider
{
    int Next(int minValue, int maxValue);
    Task<int> NextAsync(int minValue, int maxValue);
}
