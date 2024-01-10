using Application.Services;
using Application.UseCase.Queries;
using Domain.Dtos.HttpRequest;
using Domain.Dtos.Operacion;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class OperationQueryByIdTest
    {

        private readonly Mock<IOperationService> _operationServiceMock;
        private CancellationToken _cancellationToken;
        private readonly OperacionQueryByIdHandler _handler;
        public OperationQueryByIdTest()
        {
            _operationServiceMock = new Mock<IOperationService>();
            _operationServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(),It.IsAny<string>())).ReturnsAsync(new HttpRequestResponse<PlantaOperacionDto>()
            {
                Content = new PlantaOperacionDto()
                {
                    Id = 6,
                    Operacion = "Operacion",
                    Path = "Path",
                    Url = "URL",
                    Sce = "SCE"
                },
            });
            _handler = new OperacionQueryByIdHandler(_operationServiceMock.Object);
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handler_Should_ReturnExpected()
        {
            var request = new OperationQueryById();

            var result = await _handler.Handle(request, _cancellationToken);

            Assert.NotNull(result);

        }
    }
}

