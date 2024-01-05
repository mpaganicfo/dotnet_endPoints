using Andreani.Data.CQRS.Extension;
using dotnet_wos_abm_reglas_auditoria_api.Application.Common.Interfaces;
using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Configuration;
using dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Persistence;
using dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Querys;
using dotnet_wos_abm_reglas_auditoria_api.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace dotnet_wos_abm_reglas_auditoria_api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS<ApplicationDbContext>(configuration);

        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IQueryRegla, QueryRegla>();

        services.AddSingleton(new PlantaOperationConfiguration(
            configuration.GetValue<string>("PlantaOperacion:GetById"),
            configuration.GetValue<string>("PlantaOperacion:AuthorizationSchema"),
            configuration.GetValue<string>("PlantaOperacion:GetAll")));
        
        services.AddScoped<ILocationService, LocationService>();

        services.AddScoped<IOperacionService, OperacionService>();
        
        services.AddMemoryCache();

        return services;
    }
}
