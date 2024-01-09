using Domain.Dtos.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<ProvinciasDto>> FindProvinciasAsync();
    }
}
