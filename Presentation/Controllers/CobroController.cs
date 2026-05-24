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
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Cobro creado exitosamente";
            return Ok(response);
        }


        [HttpPost("individual/procesar")]
        public async Task<IActionResult> ProcesarCobroIndividual(ProcesarCobroIndividualRequest Request)
        {
            var response = await cobroService.ProcesarCobroIndividual(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Cobro individual fallido";
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Cobro individual procesado exitosamente";
            return Ok(response);
        }

        [HttpPost("lote/procesar")]
        public async Task<IActionResult> ProcesarCobroLote(ProcesarCobroLotesRequest Request)
        {
            var response = await cobroService.ProcesarCobroLote(Request);

            if (response.StatusCode != 200)
            {
                response.Message = "Cobro lote sin cambios de estado";
                return StatusCode(response.StatusCode, response);
            }

            response.Message = "Cobro lote procesado exitosamente";
            return Ok(response);
        }


    }
}
