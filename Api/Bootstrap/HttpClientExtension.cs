using dotnet_wos_abm_reglas_auditoria_api.Application.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_wos_abm_reglas_auditoria_api.Boostrap
{
    public static class HttpClientExtension
    {
        public static IServiceCollection HttpClientsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ApiNames.Location, client =>
            {
                client.BaseAddress = new System.Uri(configuration.GetValue<string>("Location:url"));
            });

            services.AddHttpClient(ApiNames.Operation, client =>
            {
                client.BaseAddress = new System.Uri(configuration.GetValue<string>("PlantaOperacion:BaseUrl"));
            });

            return services;
        }
    }
}
