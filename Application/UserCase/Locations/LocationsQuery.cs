using Andreani.ARQ.Pipeline.Clases;
using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Application.UseCase.V2.Helper;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.V2;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_wos_abm_reglas_auditoria_api.Application.UseCase.V2.Locations
{
    public class LocationsQuery : IRequest<Response<List<ProvinciasDto>>>
    {
    }

    public class LocationsQueryHandler : IRequestHandler<LocationsQuery, Response<List<ProvinciasDto>>>
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsQueryHandler> _logger;

        public LocationsQueryHandler(ILocationService locationService, ILogger<LocationsQueryHandler> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        public async Task<Response<List<ProvinciasDto>>> Handle(LocationsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProvinciasDto>>();

            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = (await _locationService.FindProvinciasAsync()).ToList();
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Notifications.Add(ex.BuildNotification(_logger, $"Get Location"));
            }

            return response;
        }
    }
}
