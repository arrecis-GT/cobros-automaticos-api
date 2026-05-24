using System.ComponentModel.DataAnnotations;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public class CreateCobroRequest
    {
        [Required]
        public int ClienteId { get; init; }

        [Required]
        public decimal Monto { get; init; }

        [Required]
        public string Moneda { get; init; } = string.Empty;

        [Required]
        public string ReferenciaExterna { get; init; } = string.Empty;
    }
}
