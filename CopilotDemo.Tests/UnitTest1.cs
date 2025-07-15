using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using CopilotDemo;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CopilotDemo.Tests
{
    // Write unit tests for GetUserHashAsync using xUnit and Moq
    public class UserHashServiceTests
    {
        private readonly Mock<ILogger<UserHashService>>? loggerMock;
        public UserHashServiceTests()
        {
            loggerMock = new Mock<ILogger<UserHashService>>();
        }

        [Fact]
        public async Task GetUserHashAsync_ReturnsUserHash_WhenEntityExists()
        {
            // Arrange
            var uid = "test-uid";
            var expectedUserHash = new UserHash { RowKey = "UserHash" };

            var tableClientMock = new Mock<TableClient>();
            var responseMock = new Mock<Response<UserHash>>();
            responseMock.Setup(r => r.Value).Returns(expectedUserHash);

            tableClientMock
                .Setup(tc => tc.GetEntityAsync<UserHash>("users", uid, null, default))
                .ReturnsAsync(responseMock.Object);

            //var loggerMock = new Mock<ILogger<UserHashService>>();
            var service = new UserHashService(tableClientMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetUserHashAsync(uid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserHash.RowKey, result.RowKey);
        }

        [Fact]
        public async Task GetUserHashAsync_ReturnsNull_WhenEntityNotFound()
        {
            // Arrange
            var uid = "missing-uid";
            var tableClientMock = new Mock<TableClient>();

            tableClientMock
                .Setup(tc => tc.GetEntityAsync<UserHash>("users", uid, null, default))
                .ThrowsAsync(new RequestFailedException(404, "Not found"));

            //var loggerMock = new Mock<ILogger<UserHashService>>();
            var service = new UserHashService(tableClientMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetUserHashAsync(uid);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserHashAsync_ThrowsException_OnOtherErrors()
        {
            // Arrange
            var uid = "error-uid";
            var tableClientMock = new Mock<TableClient>();

            tableClientMock
                .Setup(tc => tc.GetEntityAsync<UserHash>("users", uid, null, default))
                .ThrowsAsync(new RequestFailedException(500, "Server error"));

            //var loggerMock = new Mock<ILogger<UserHashService>>();
            var service = new UserHashService(tableClientMock.Object, loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<RequestFailedException>(() => service.GetUserHashAsync(uid));
        }
    }
}
