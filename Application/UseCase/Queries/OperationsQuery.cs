using Application.Services;
using Domain.Dtos.HttpRequest;
using Domain.Dtos.Operacion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries
{
   
        public class OperationsQuery : IRequest<HttpRequestResponse<IEnumerable<PlantaOperacionDto>>>
        {
        }

        public class OperationsQueryHandler : IRequestHandler<OperationsQuery, HttpRequestResponse<IEnumerable<PlantaOperacionDto>>>
        {
            private readonly IOperationService _operationService;

            public OperationsQueryHandler(IOperationService operationService)
            {
                _operationService = operationService;
            }

            public async Task<HttpRequestResponse<IEnumerable<PlantaOperacionDto>>> Handle(OperationsQuery request, CancellationToken cancellationToken)
            {
                return await _operationService.GetAllAsync("token");
            }
        }
    
}
