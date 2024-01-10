using Domain.Dtos.Operacion;
using Infrastructure.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Infrastructure.Test.Services
{
    public class OperacionServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpFactory;
        private readonly PlantaOperationConfiguration _plantaOperacionConfiguration;
        private OperacionService _operacionService;
        private readonly int _id = 5;
        private readonly Mock<ILogger<OperacionService>> _logger;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OperacionServiceTests()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _plantaOperacionConfiguration = new PlantaOperationConfiguration("GetById/{0}", "Bearer", "getAll");
            _httpFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger<OperacionService>>();
        }

        [Fact]
        public async Task GetById_ReturnExpected()
        {
            //arrange
            var expected = _id;
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonSerializer.Serialize(GetDto()))
               })
               .Verifiable();

            handlerMock
               .Protected()
               .Setup("Dispose", ItExpr.IsAny<bool>());

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            _operacionService = new OperacionService(_httpFactory.Object, _logger.Object, _plantaOperacionConfiguration);

            //act
            var response = await _operacionService.GetByIdAsync(_id, "token");

            //assert

            Assert.NotNull(response);
            Assert.Null(response.Message);
            Assert.True(response.Success);
            Assert.NotNull(response.Content);
            Assert.Equal(expected, response.Content.Id);
        }

        [Fact]
        public async Task GetAll_ReturnExpected()
        {
            //arrange
            var expected = _id;
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonSerializer.Serialize(new[] { GetDto() }))
               })
               .Verifiable();

            handlerMock
               .Protected()
               .Setup("Dispose", ItExpr.IsAny<bool>());

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            _operacionService = new OperacionService(_httpFactory.Object, _logger.Object, _plantaOperacionConfiguration);

            //act
            var response = await _operacionService.GetAllAsync("token");

            //assert

            Assert.NotNull(response);
            Assert.Null(response.Message);
            Assert.True(response.Success);
            Assert.NotNull(response.Content);
            Assert.True(response.Content.Any());
        }

        private PlantaOperacionDto GetDto()
        {
            return new PlantaOperacionDto()
            {
                Id = _id,
                Operacion = "Test",
                Path = "Path",
                Sce = "Sce",
                Url = "Url"
            };
        }
    }
}
