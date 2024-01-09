using Domain.Dtos.HttpRequest;
using Domain.Dtos.Operacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IOperationService
    {
        //Task<HttpRequestResponse<PlantaOperacionDto>> GetByIdAsync(int id, string token);

        Task<HttpRequestResponse<IEnumerable<PlantaOperacionDto>>> GetAllAsync(string token);
    }
}
