
using dotnet_endPoints.Dtos.Operacion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OperacionesController
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = WebHostConstants.AzureAdB2cAuthenticationScheme)]
    public class OperacionesController : ApiControllerBase
    {

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(List<PlantaOperacionDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] int? start, int? length)
        {
            var contratoResponse = await Mediator.Send(new GetAllOperacionesQuery() { Start = start ?? 0, Length = length ?? 10, RuleType = _rule, Token = Authorization });
            var contratoMapped = MapResponse(contratoResponse, new Response<List<PlantaOperacionDto>>());
            contratoMapped.Content = contratoResponse.Content?.Select(x => x as PlantaOperacionDto).ToList();

            return Result(contratoMapped);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReglaContratoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BaseReglaContratoDto request)
        {
            return Result(await Mediator.Send(new CreateReglaCommand() { Rule = request, RuleType = _rule }));
        }

    }
}
