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

            if (response.StatusCode != 200)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


    }
}
