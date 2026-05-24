using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Application.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CobrosAutomaticosApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClientService clientService;
        private readonly ICobroService cobroService;

        public ClienteController(IClientService clientService, ICobroService cobroService)
        {
            this.clientService = clientService;
            this.cobroService = cobroService;
        }


        [HttpPost("CrearClient")]
        public async Task<IActionResult> CrearClient(CreateClientRequest Request)
        {
            var findClient = await clientService.FindClientByDpi(Request.Dpi);

            if (findClient)
            {

                return StatusCode(400, new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Error, el cliente ya existe"
                });
            }


            var response = await clientService.CrearClient(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Error al crear el cliente";
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Cliente creado exitosamente";
            return Ok(response);
        }


        [HttpGet("{ClienteId}/cobros")]
        public async Task<IActionResult> ListarCobros([FromRoute] int ClienteId, [FromQuery] DateOnly? FechaInicio = null, [FromQuery] DateOnly? FechaFin = null)
        {
            var response = await cobroService.ListarCobros(ClienteId, FechaInicio, FechaFin);

            if (response.StatusCode != 200)
            {
                response.Message = "No se encontraron cobros para el cliente";
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Cobros listados exitosamente";
            return Ok(response);
        }

    }
}
