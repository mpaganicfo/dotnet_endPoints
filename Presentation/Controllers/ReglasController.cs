using Application.UseCase.Locations;
using Domain.Dtos.Locations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            var respuesta = await this.mediator.Send(new LocationsQuery());

            return Ok(respuesta);
        }
    }
}
