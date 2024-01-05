using dotnet_endPoints.Domain.Dtos.HttpRequest;
using dotnet_endPoints.Domain.Dtos.Operacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_endPoints.Application.Services
{
    public interface IOperacionService
    {
        Task<HttpRequestResponse<PlantaOperacionDto>> GetByIdAsync(int id, string token);

        Task<HttpRequestResponse<IEnumerable<PlantaOperacionDto>>> GetAllAsync(string token);
    }
}
