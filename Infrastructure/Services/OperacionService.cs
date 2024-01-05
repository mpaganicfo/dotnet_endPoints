using dotnet_wos_abm_reglas_auditoria_api.Application.Common;
using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.HttpRequest;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Operacion;
using dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Slapper.AutoMapper;

namespace dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Services
{
    public class OperacionService : IOperacionService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<OperacionService> _logger;
        private readonly PlantaOperationConfiguration _config;

        public OperacionService(IHttpClientFactory factory, ILogger<OperacionService> logger, PlantaOperationConfiguration config)
        {
            _factory = factory;
            _config = config;
            _logger = logger;
        }

        public async Task<HttpRequestResponse<PlantaOperacionDto>> GetByIdAsync(int id, string token)
        {
            try
            {
                var url = string.Format(_config.GetByIdTemplate, id);
                _logger.LogInformation("GetByIdOperation, requestUrl: {url}", url);
                var request = await RequestAsync(url, token);
                _logger.LogInformation("GetByIdOperation result  StatusCode: {StatusCode}, ReasonPhrase: {ReasonPhrase}", request.StatusCode, request.ReasonPhrase);

                if (!request.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse<PlantaOperacionDto>()
                    {
                        Message = $"No se ha podido Obtener la planta de Operacion con Id {id}: {request.ReasonPhrase}"
                    };
                }

                return new HttpRequestResponse<PlantaOperacionDto>()
                {
                    Content = await DeserealizeAsync<PlantaOperacionDto>(request.Content)
                };
            }
            catch (Exception ex)
            {

                return new HttpRequestResponse<PlantaOperacionDto>()
                {
                    Message = GetErrorFromException(ex, $"Se ha producido un error obteniendo la Platan de operacion por Id {id}: {ex.Message} ")
                };
            }
        }

        public async Task<HttpRequestResponse<IEnumerable<PlantaOperacionDto>>> GetAllAsync(string token)
        {
            try
            {
                _logger.LogInformation("GetAllAsync, requestUrl: {url}", _config.GetAll);
                var request = await RequestAsync(_config.GetAll, token);
                _logger.LogInformation("GetAllAsync result  StatusCode: {StatusCode}, ReasonPhrase: {ReasonPhrase}", request.StatusCode, request.ReasonPhrase);
                
                if (!request.IsSuccessStatusCode)
                {
                    return new HttpRequestResponse<IEnumerable<PlantaOperacionDto>>()
                    {
                        Message = $"No se ha podido Obtener las plantas de Operaciones: {request.ReasonPhrase}"
                    };
                }
                    
                return new HttpRequestResponse<IEnumerable<PlantaOperacionDto>>()
                {
                    Content = await DeserealizeAsync<IEnumerable<PlantaOperacionDto>>(request.Content)
                };
            }
            catch (Exception ex)
            {

                return new HttpRequestResponse<IEnumerable<PlantaOperacionDto>>()
                {
                    Message = GetErrorFromException(ex, $"Se ha producido un error obteniendo las Platas de operaciones: {ex.Message} ")
                };
            }
        }

        private async Task<T> DeserealizeAsync<T>(HttpContent content)
        {
            var result = await content.ReadAsStringAsync();
            _logger.LogInformation("result: {result}", result);

            return JsonSerializer.Deserialize<T>(result,
                                    new JsonSerializerOptions()
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
        }

        private async Task<HttpResponseMessage> RequestAsync(string url, string token)
        {
            using (var client = CreateClient(token))
            {
                return await client.GetAsync(url);
            }
        }

        private HttpClient CreateClient(string token)
        {
            var client = _factory.CreateClient(ApiNames.Operation);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_config.AuthorizationSchema, token);
            return client;
        }

        private static string GetErrorFromException(Exception exception, string messageTemplate)
        {
            var stringBuilder = new StringBuilder(messageTemplate);
            stringBuilder.AppendLine($"Stack Trace: {exception.StackTrace}");

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                stringBuilder.AppendLine($"Inner Exception: {innerException.Message}");
                innerException = innerException.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
