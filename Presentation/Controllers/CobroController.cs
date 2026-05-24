using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CobrosAutomaticosApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobroController : ControllerBase
    {
        private readonly ICobroService cobroService;


        public CobroController(ICobroService cobroService)
        {
            this.cobroService = cobroService;
        }


        [HttpPost("CrearCobro")]
        public async Task<IActionResult> CrearCobro(CreateCobroRequest Request)
        {
            var response = await cobroService.CrearCobro(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Error al crear el cobro";
                return BadRequest(response);
            }

            response.Message = "Cobro creado exitosamente";
            return Ok(response);
        }


        [HttpGet("ListarCobros/{ClienteId}")]
        public async Task<IActionResult> ListarCobros([FromRoute] int ClienteId)
        {
            var response = await cobroService.ListarCobros(ClienteId);

            if (response.StatusCode != 200)
            {
                response.Message = "No se encontraron cobros para el cliente";
                return BadRequest(response);
            }

            response.Message = "Cobros listados exitosamente";
            return Ok(response);
        }       

    }
}
