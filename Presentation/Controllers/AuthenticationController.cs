using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CobrosAutomaticosApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _AuthService;

        public AuthenticationController(IAuthenticationService AuthService)
        {
            _AuthService = AuthService;
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest Request)
        {
            var response = await _AuthService.LogIn(Request);

            if (response.StatusCode == 401)
            {
                response.Message = "Usuario o contraseña incorrecto";
                return StatusCode(response.StatusCode, response);
            }

            if (response.StatusCode == 302)
            {
                response.Message = "El usuario ya tiene una sesión activa";
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Login exitoso";
            return Ok(response);
        }


    }
}
