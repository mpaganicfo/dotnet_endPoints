using dotnet_wos_abm_reglas_auditoria_api.Application.UseCase.V2.Locations;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReglasController : Controller
    {
        private readonly IMediator mediator;

        public ReglasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("locations")]
        [ProducesResponseType(typeof(List<ProvinciasDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Locations()
        {
            return Ok(await this.mediator.Send(new LocationsQuery()));
        }
    }
}
