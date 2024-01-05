using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Operacion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.Create
{
    public class CreateOperacionCommand : BaseCommand, IRequest<Response<PlantaOperacionDto>>
    {
        public PlantaOperacionDto Rule { get; set; }
    }

    public class CreateOperacionCommandHandler : BaseCreate, IRequestHandler<CreateOperacionCommand, Response<PlantaOperacionDto>>
    {
        private readonly ILogger<CreateOperacionCommandHandler> _logger;

        public CreateOperacionCommandHandler(ILogger<CreateOperacionCommandHandler> logger, ITransactionalRepository repository) : base(repository)
        {
            _logger = logger;
        }

        public async Task<Response<PlantaOperacionDto>> Handle(CreateOperacionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<PlantaOperacionDto>();

            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = (await CreateAsync(request.Rule, request.RuleType)).Map();
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Notifications.Add(ex.BuildNotification(_logger, $"Create {request.RuleType.GetFriendlyName()}"));
            }

            return response;
        }
    }
}
