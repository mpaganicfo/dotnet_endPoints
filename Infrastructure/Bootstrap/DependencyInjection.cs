
using Application.Services;
using Infrastructure.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Bootstrap;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOperationService, OperacionService>();
        services.AddSingleton(new PlantaOperationConfiguration(
    configuration.GetValue<string>("PlantaOperacion:GetById"),
    configuration.GetValue<string>("PlantaOperacion:AuthorizationSchema"),
    configuration.GetValue<string>("PlantaOperacion:GetAll")));
        services.AddMemoryCache();

        return services;
    }
}
