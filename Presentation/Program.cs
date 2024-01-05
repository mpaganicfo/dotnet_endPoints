
using Application;
using Application.Services;
using dotnet_wos_abm_reglas_auditoria_api.Application.Services;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR((typeof(Program).Assembly));
builder.Services.AddTransient<ILocationService, LocationService>();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient(ApiNames.Location, client =>
{
    client.BaseAddress = new System.Uri("www.baseUrl");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
