namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; init; }

        public string UserName { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public char Status { get; init; }   = 'A';

    }
}
