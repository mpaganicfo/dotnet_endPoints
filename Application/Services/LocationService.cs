using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Domain.Dtos.Locations;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IMemoryCache _cache;

        public LocationService(IHttpClientFactory factory, IMemoryCache cache)
        {
            _factory = factory;
            _cache = cache;
        }

        public async Task<IEnumerable<ProvinciasDto>> FindProvinciasAsync()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _cache.GetOrCreateAsync(ApiNames.Location, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);

                var data = await FetchDataFromSource();
                return data;
            });
        }

        private async Task<IEnumerable<ProvinciasDto>> FetchDataFromSource()
        {
            using (var client = _factory.CreateClient(ApiNames.Location))
            {
                var result = await client.GetAsync(string.Empty);
                if (result.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<LocationResponse>>(await result.Content.ReadAsStringAsync());
                    return response
                                    .GroupBy(loc => loc.provincia)
                                    .Select(group => new ProvinciasDto
                                    {
                                        Nombre = group.Key,
                                        Localidades = group.Select(loc => new LocalidadesDto
                                        {
                                            Nombre = loc.localidad,
                                            CodigosPostales = loc.codigosPostales
                                        }).ToArray()
                                    }).ToArray();
                }

                throw new Exception(result.ReasonPhrase);
            }
        }
    }
}