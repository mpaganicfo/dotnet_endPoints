using Application.Services;
using Application.UseCase.Locations;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Domain.Dtos.Locations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Test
{
    public class LocationQueryHandlerTest
    {
        private readonly Mock<ILocationService> _locationServiceMock;
        private CancellationToken _cancellationToken;
        private readonly LocationsQueryHandler _handler;
        public LocationQueryHandlerTest()
        {
            _locationServiceMock = new Mock<ILocationService>();
            _locationServiceMock.Setup(x => x.FindProvinciasAsync()).ReturnsAsync(new List<ProvinciasDto>
            {
                new ProvinciasDto
                {
                    Nombre = "Buenos Aires",
                    Localidades = new LocalidadesDto[]
                    {
                        new LocalidadesDto
                        {
                            Nombre = "Localidad1",
                            CodigosPostales = new[] { "12345", "54321" }
                        },
                        new LocalidadesDto
                        {
                            Nombre = "Localidad2",
                            CodigosPostales = new[] { "67890", "09876" }
                        }
                    }
                }
            });

            _handler = new LocationsQueryHandler(_locationServiceMock.Object);
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handler_Should_ReturnExpected()
        {
            // arrange
            var request = new LocationsQuery();

            // act
            var result = await _handler.Handle(request, _cancellationToken);

            // assert
            Assert.NotNull(result); 

        }
    }



}
