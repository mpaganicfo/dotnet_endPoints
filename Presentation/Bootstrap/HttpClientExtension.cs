using Application.Common;
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
                client.BaseAddress = new System.Uri(configuration.GetValue<string>("Location:url"));
            });

            return services;
        }
    }
}
