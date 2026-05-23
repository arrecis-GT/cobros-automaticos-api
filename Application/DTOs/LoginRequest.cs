using System.ComponentModel.DataAnnotations;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; init; } = string.Empty;
        
        [Required]
        public string Password { get; init; } = string.Empty;
    }
}
