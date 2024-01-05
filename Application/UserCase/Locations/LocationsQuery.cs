using Application.Services;
using Domain.Dtos.Locations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.Locations
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
