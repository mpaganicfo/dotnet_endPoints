using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_wos_abm_reglas_auditoria_api.Application.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<ProvinciasDto>> FindProvinciasAsync();
    }
}
