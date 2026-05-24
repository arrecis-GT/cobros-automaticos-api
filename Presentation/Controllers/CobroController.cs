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
        public async Task<IActionResult> CreateCobro(CreateCobroRequest Request)
        {
            var response = await cobroService.CreateCobro(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Error al crear el cobro";
                return BadRequest(response);
            }

            response.Message = "Cobro creado exitosamente";
            return Ok(response);
        }

    }
}
