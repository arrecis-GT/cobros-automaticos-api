using System.ComponentModel.DataAnnotations;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public record ProcesarCobroLotesRequest
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int[] CobroIds { get; set; } = new int[0];
    }
}
