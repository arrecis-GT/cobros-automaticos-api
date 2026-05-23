using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Application.Services;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using CobrosAutomaticosApi.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection
builder.Services.AddSingleton<ConnexionDB>();

// Repositories
builder.Services.AddScoped<AuthenticationRepository>();

// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
