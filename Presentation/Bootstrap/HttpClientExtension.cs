using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Bootstrap
{
    public static class HttpClientExtension
    {
        public static IServiceCollection HttpClientsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ApiNames.Location, client =>
            {
                //client.BaseAddress = new System.Uri(configuration.GetValue<string>("Location:url"));
                client.BaseAddress = new System.Uri("https://apisqa.andreani.com/v1/localidades");
            });

            return services;
        }
    }
}
