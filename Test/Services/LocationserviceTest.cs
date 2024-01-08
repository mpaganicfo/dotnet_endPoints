
using Application.Services;
using Domain.Dtos.Locations;
using Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Infrastructure.Test.Services
{
    public class LocationServiceTest
    {
        private readonly Mock<IHttpClientFactory> _factory;
        private readonly ILocationService _locationService;

        public LocationServiceTest()
        {
            var memoryCache = Mock.Of<IMemoryCache>();
            var cachEntry = Mock.Of<ICacheEntry>();

            var mockMemoryCache = Mock.Get(memoryCache);
            mockMemoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cachEntry);


            _factory = new Mock<IHttpClientFactory>();

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
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
                   Content = new StringContent(JsonSerializer.Serialize(new List<LocationResponse>()
                   {
                       new LocationResponse()
                       {
                           provincia = "Buenos Aires",
                           localidad = "Florida",
                           partido = "Vicente Lopez",
                           codigosPostales = new string[] {"1602"}
                       }
                   }))
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _factory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            _locationService = new LocationService(_factory.Object, mockMemoryCache.Object);
        }

        [Fact]
        public async Task FindProvincias_ReturnExpected()
        {
            //arrange

            //act

            var result = await _locationService.FindProvinciasAsync();

            //assert

            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}