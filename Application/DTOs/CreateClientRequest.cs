using System.ComponentModel.DataAnnotations;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public record CreateClientRequest
    {
        [Required]
        public string Dpi { get; init; } = string.Empty;

        [Required]
        public string Nombre { get; init; } = string.Empty;

        [Required]
        public string Email { get; init; } = string.Empty;

        [Required]
        public string Telefono { get; init; } = string.Empty;
    }
}
