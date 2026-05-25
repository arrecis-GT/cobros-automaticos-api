using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using CobrosAutomaticosApi.Infraestructure.Repositories;

namespace CobrosAutomaticosApi.Infraestructure.Persistence.Filters
{
    public class ValidateSessionFilter : IAsyncAuthorizationFilter
    {
        private readonly AuthenticationRepository _repository;
        private readonly IConfiguration _config;

        public ValidateSessionFilter(AuthenticationRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 1. Excluir el controlador de Autenticación para evitar bloquear el Login
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            if (controllerName != null && controllerName.Equals("Authentication", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // 2. Validar que la petición ya cuente con un JWT válido y autenticado
            var user = context.HttpContext.User;
            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "No esta autorizado para utilizar esta función." });
                return;
            }

            // Extraer el username desde los claims del JWT
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "No esta autorizado para utilizar esta función." });
                return;
            }

            // 3. Validar la existencia de la sesión activa en la base de datos
            var sesion = await _repository.CheckExistSession(userName);
            if (sesion == null)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "No esta autorizado para utilizar esta función." });
                return;
            }

            // Zona Horaria de Guatemala (Central America)
            TimeZoneInfo guatemalaZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            DateTime horaGuate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, guatemalaZone);

            // 4. Validar que la sesión pertenezca al día actual
            var fechaActual = DateOnly.FromDateTime(horaGuate);
            if (sesion.FechaCreacion != fechaActual)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "La sesión ha expirado, ingrese nuevamente." });
                return;
            }

            // 5. Validar el TimeOut por inactividad (Hora actual vs Última Conexión)
            int timeOutSegundos = _config.GetValue<int>("Session:TimeOut");
            var horaActual = TimeOnly.FromDateTime(horaGuate);

            TimeSpan tiempoInactividad = horaActual - sesion.UltimaConexion;

            if (tiempoInactividad.TotalSeconds > timeOutSegundos)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "La sesión ha expirado, ingrese nuevamente." });
                return;
            }

            // 6. Si todo es válido se actualizar la hora de última conexión en la base de datos
            await _repository.UpdateLastConnection(userName);
        }
    }
}