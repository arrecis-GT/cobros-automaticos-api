using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Application.Services;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using CobrosAutomaticosApi.Infraestructure.Persistence.Filters;
using CobrosAutomaticosApi.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection
builder.Services.AddSingleton<ConnexionDB>();

// Repositories
builder.Services.AddScoped<AuthenticationRepository>();
builder.Services.AddScoped<CobroRepository>();
builder.Services.AddScoped<ClienteRespository>();

// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICobroService, CobroService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]!)),
        ValidateIssuer = false, 
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateSessionFilter>();
});

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
