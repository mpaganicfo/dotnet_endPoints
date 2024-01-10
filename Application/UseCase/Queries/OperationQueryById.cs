using Application.Services;
using Domain.Dtos.HttpRequest;
using Domain.Dtos.Operacion;
using MediatR;

namespace Application.UseCase.Queries
{
    public class OperationQueryById : IRequest<HttpRequestResponse<PlantaOperacionDto>>
        {
        }

        public class OperacionQueryByIdHandler : IRequestHandler<OperationQueryById, HttpRequestResponse<PlantaOperacionDto>>
        {
            private readonly IOperationService _operationService;

            public OperacionQueryByIdHandler(IOperationService operationService)
            {
                _operationService = operationService;
            }

            public async Task<HttpRequestResponse<PlantaOperacionDto>> Handle(OperationQueryById request, CancellationToken cancellationToken)
            {
                return await _operationService.GetByIdAsync(1,"token");
            }
        }
    }
