using Application.Services;
using Application.UseCase.Queries;
using Domain.Dtos.HttpRequest;
using Domain.Dtos.Operacion;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class OperationsQueryHandlerTest
    {
        private readonly Mock<IOperationService> _operationServiceMock;
        private CancellationToken _cancellationToken;
        private readonly OperationsQueryHandler _handler;
        public OperationsQueryHandlerTest()
        {
            _operationServiceMock = new Mock<IOperationService>();
            _operationServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(new HttpRequestResponse<IEnumerable<PlantaOperacionDto>>()
            {
                Content = new[]{new PlantaOperacionDto()
                {
                    Id = 6,
                    Operacion = "Operacion",
                    Path = "Path",
                    Url = "URL",
                    Sce = "SCE"
                }},
            });
            _handler = new OperationsQueryHandler(_operationServiceMock.Object);
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task Handler_Should_ReturnExpected()
        {
            var request = new OperationsQuery();

            var result = await _handler.Handle(request, _cancellationToken);

            Assert.NotNull(result);
            
        }
    }
}
