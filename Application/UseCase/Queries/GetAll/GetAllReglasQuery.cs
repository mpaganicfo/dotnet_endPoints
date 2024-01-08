
using Application.Services;
using Domain.Dtos;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.GetAll
{
    public class GetAllReglasQuery : BaseQuery, IRequest<Response<List<BaseReglaDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }
    }

    public class GetAllReglasQueryHandler : BaseGetAllReglasQuery, IRequestHandler<GetAllReglasQuery, Response<List<BaseReglaDto>>>
    {
        private readonly ILogger<GetAllReglasQueryHandler> _logger;

        public GetAllReglasQueryHandler(ILogger<GetAllReglasQueryHandler> logger, IReadOnlyQuery query, IOperacionService operacionService) : base(query, operacionService)
        {
            _logger = logger;
        }

        public async Task<Response<List<BaseReglaDto>>> Handle(GetAllReglasQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<BaseReglaDto>>();

            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = (await AddOperationAsync((await GetAllAsync(request.Start, request.Length, request.RuleType)), request.Token))?.ToList();
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Notifications.Add(ex.BuildNotification(_logger, $"GetAll {request.RuleType.GetFriendlyName()}"));
            }

            return response;
        }
    }
}
