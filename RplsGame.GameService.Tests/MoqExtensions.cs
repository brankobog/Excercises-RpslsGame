using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System.Net.Http.Json;

namespace RplsGame.GameService.Tests
{
    public static class MoqExtensions
    {
        public static ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupRequest(this Mock<HttpMessageHandler> mock, HttpMethod method, string requestUri)
        {
            return mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == method && req.RequestUri.ToString() == requestUri),
                    ItExpr.IsAny<CancellationToken>());
        }

        public static IReturnsResult<HttpMessageHandler> ReturnsJsonResponse<T>(this ISetup<HttpMessageHandler, Task<HttpResponseMessage>> setup, T content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(content)
            };
            return setup.ReturnsAsync(response);
        }
    }
}
