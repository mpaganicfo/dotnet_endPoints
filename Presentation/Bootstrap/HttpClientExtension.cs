using Application.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Boostrap
{
    public static class HttpClientExtension
    {
        public static IServiceCollection HttpClientsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient(ApiNames.Operation, client =>
            {
                client.BaseAddress = new System.Uri(configuration.GetValue<string>("PlantaOperacion:BaseUrl"));
            });

            return services;
        }
    }
}
