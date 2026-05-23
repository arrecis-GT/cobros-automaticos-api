namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Sesion
    {
        public int SesionId { get; init; }

        public int UsuarioId { get; init; }

        public string Token { get; init; } = string.Empty;

        public DateOnly FechaCreacion { get; init; }

        public TimeOnly HoraCreacion { get; init; }

        public TimeOnly UltimaConexion { get; init; }

        public char Status { get; init; } = 'A';
    }
}
