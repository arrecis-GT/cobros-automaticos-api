using System.ComponentModel.DataAnnotations;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public record ProcesarCobroIndividualRequest
    {
        [Required]
        public int UsuarioId { get; init; }

        [Required]
        public int CobroId { get; init; }

    }
}
