
using dotnet_endPoints.Application.Services;
using dotnet_endPoints.Application.UseCase.V2.Helper;
using dotnet_endPoints.Domain.Dtos.Operacion;
using dotnet_endPoints.Domain.Dtos;
using dotnet_endPoints.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_endPoints.Application.UseCase.Queries.GetAll
{
    public class GetAllOperacionesQuery : BaseQuery, IRequest<Response<List<PlantaOperacionDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }
    }

    public class GetAllReglasQueryHandler : BaseGetAllReglasQuery, IRequestHandler<GetAllOperacionesQuery, Response<List<BaseReglaDto>>>
    {
        private readonly ILogger<GetAllReglasQueryHandler> _logger;

        public GetAllReglasQueryHandler(ILogger<GetAllReglasQueryHandler> logger, IReadOnlyQuery query, IOperacionService operacionService) : base(query, operacionService)
        {
            _logger = logger;
        }

        public async Task<Response<List<BaseReglaDto>>> Handle(GetAllOperacionesQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<BaseReglaDto>>();

            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = (await AddOperationAsync((await GetAllAsync(request.Start, request.Length, request.RuleType)), request.Token))?.ToList();
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Notifications.Add(ex.BuildNotification(_logger, $"GetAll {request.RuleType.GetFriendlyName()}"));
            }

            return response;
        }
    }
}
