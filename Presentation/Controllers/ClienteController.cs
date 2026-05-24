using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CobrosAutomaticosApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClientService clientService;

        public ClienteController(IClientService clientService)
        {
            this.clientService = clientService;
        }


        [HttpPost("CrearClient")]
        public async Task<IActionResult> CrearClient(CreateClientRequest Request)
        {
            var findClient = await clientService.FindClientByDpi(Request.Dpi);

            if (findClient)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Error, el cliente ya existe"
                });
            }


            var response = await clientService.CrearClient(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Error al crear el cliente";
                return BadRequest(response);
            }

            response.Message = "Cliente creado exitosamente";
            return Ok(response);
        }

    }
}
