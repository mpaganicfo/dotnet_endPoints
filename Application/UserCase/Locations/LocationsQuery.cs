using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations;
using MediatR;

namespace dotnet_wos_abm_reglas_auditoria_api.Application.UseCase.V2.Locations
{
    public class LocationsQuery : IRequest<List<ProvinciasDto>>
    {
    }

    public class LocationsQueryHandler : IRequestHandler<LocationsQuery, List<ProvinciasDto>>
    {
        private readonly ILocationService _locationService;

        public LocationsQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<ProvinciasDto>> Handle(LocationsQuery request, CancellationToken cancellationToken)
        {
            return (await _locationService.FindProvinciasAsync()).ToList();
        }
    }
}
