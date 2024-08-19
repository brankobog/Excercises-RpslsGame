using RpslsGame.GameService.Randomness;

namespace RplsGame.GameService.Tests.Unit.Randomness;
public class SystemRandomnessProviderTests
{
    private readonly SystemRandomnessProvider _randomnessProvider;

    public SystemRandomnessProviderTests()
    {
        _randomnessProvider = new SystemRandomnessProvider();
    }

    [Fact]
    public void Next_ShouldReturnValueWithinRange()
    {
        // Arrange
        int minValue = 1;
        int maxValue = 10;

        // Act
        int result = _randomnessProvider.Next(minValue, maxValue);

        // Assert
        Assert.InRange(result, minValue, maxValue - 1);
    }

    [Fact]
    public async Task NextAsync_ShouldReturnValueWithinRange()
    {
        // Arrange
        int minValue = 1;
        int maxValue = 10;

        // Act
        int result = await _randomnessProvider.NextAsync(minValue, maxValue);

        // Assert
        Assert.InRange(result, minValue, maxValue - 1);
    }
}
