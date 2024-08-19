using Microsoft.Extensions.Logging;
using Moq;
using RpslsGame.GameService.Randomness;

namespace RplsGame.GameService.Tests.Unit.Randomness
{
    public class WebApiRandomnessProviderTests
    {
        private readonly Mock<ILogger<WebRandomnessProvider>> _mockLogger;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly WebRandomnessProvider _randomnessProvider;

        public WebApiRandomnessProviderTests()
        {
            _mockLogger = new Mock<ILogger<WebRandomnessProvider>>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };
            _randomnessProvider = new WebRandomnessProvider(_mockLogger.Object, _httpClient);
        }

        [Fact]
        public async Task NextAsync_ShouldReturnValueWithinRange()
        {
            // Arrange
            int minValue = 1;
            int maxValue = 10;
            var randomResponse = new RandomResponseDto { Value = 50 };
            _mockHttpMessageHandler
                .SetupRequest(HttpMethod.Get, "http://localhost/random")
                .ReturnsJsonResponse(randomResponse);

            // Act
            int result = await _randomnessProvider.NextAsync(minValue, maxValue);

            // Assert
            Assert.InRange(result, minValue, maxValue - 1);
        }

        [Fact]
        public void Next_ShouldReturnValueWithinRange()
        {
            // Arrange
            int minValue = 1;
            int maxValue = 10;
            var randomResponse = new RandomResponseDto { Value = 50 };
            _mockHttpMessageHandler.SetupRequest(HttpMethod.Get, "http://localhost/random")
                .ReturnsJsonResponse(randomResponse);

            // Act
            int result = _randomnessProvider.Next(minValue, maxValue);

            // Assert
            Assert.InRange(result, minValue, maxValue - 1);
        }

        [Fact]
        public async Task NextAsync_ShouldThrowException_WhenResponseIsNull()
        {
            // Arrange
            int minValue = 1;
            int maxValue = 10;
            _mockHttpMessageHandler.SetupRequest(HttpMethod.Get, "http://localhost/random")
                .ReturnsJsonResponse(default(RandomResponseDto));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _randomnessProvider.NextAsync(minValue, maxValue));
        }

        [Fact]
        public void Next_ShouldThrowException_WhenResponseIsNull()
        {
            // Arrange
            int minValue = 1;
            int maxValue = 10;
            _mockHttpMessageHandler.SetupRequest(HttpMethod.Get, "http://localhost/random")
                .ReturnsJsonResponse(default(RandomResponseDto));

            // Act & Assert
            Assert.Throws<AggregateException>(() => _randomnessProvider.Next(minValue, maxValue));
        }

        [Theory]
        [InlineData(0, 0, 9, 0)]
        [InlineData(50, 0, 9, 4)]
        [InlineData(15, 0, 9, 1)]
        [InlineData(0, 0, 5, 0)]
        [InlineData(25, 0, 5, 1)]
        [InlineData(50, 0, 5, 2)]
        public void Normalize_ShouldReturnValueWithinRange(int value, int minValue, int maxValue, int expected)
        {
            // Act & Assert
            Assert.Equal(expected, WebRandomnessProvider.NormalizeToRange(value, minValue, maxValue));
        }
    }
}
