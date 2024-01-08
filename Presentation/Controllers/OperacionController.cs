using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UseCase.Queries.GetAll;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly IMediator mediator;
        public OperacionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Operations()
        {
            var response = await this.mediator.Send(request: new GetAllReglasQuery());
            return Ok(response);
        }

    }
}
