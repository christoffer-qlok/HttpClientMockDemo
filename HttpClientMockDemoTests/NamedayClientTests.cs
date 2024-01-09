using HttpClientMockDemo;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientMockDemoTests
{
    [TestClass]
    public class NamedayClientTests
    {
        [TestMethod]
        public async Task GetTodaysNameday_ReturnsCorrectNameday()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"day\": 9,\"month\": 1,\"nameday\": {\"se\": \"test-person\"},\"country\": \"se\"}")
                });

            // Create new mock HttpClient based on the mock HttpMessageHandler
            HttpClient mockClient = new HttpClient(mockHandler.Object);

            // Create a new NamedayClient that uses our mock HttpClient
            NamedayClient client = new NamedayClient(mockClient);

            // Act
            string result = await client.GetTodaysNameday();

            // Assert
            Assert.AreEqual("test-person", result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetTodaysNameday_ExceptionOnBadStatusCode()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            // Create new mock HttpClient based on the mock HttpMessageHandler
            HttpClient mockClient = new HttpClient(mockHandler.Object);

            // Create a new NamedayClient that uses our mock HttpClient
            NamedayClient client = new NamedayClient(mockClient);

            // Act
            await client.GetTodaysNameday();
        }
    }
}
