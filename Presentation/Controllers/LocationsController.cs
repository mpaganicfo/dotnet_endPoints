using Application.UseCase.Locations;
using Domain.Dtos.Locations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly IMediator mediator;

        public LocationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProvinciasDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> GetLocations()
        {

            var respuesta = await this.mediator.Send(new LocationsQuery());

            return Ok(respuesta);
        }
    }
}
