namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Sesion
    {
        public int SesionId { get; set; }

        public int UsuarioId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateOnly FechaCreacion { get; set; }

        public TimeOnly HoraCreacion { get; set; }

        public TimeOnly UltimaConexion { get; set; }

        public char Status { get; set; } = 'A';
    }
}
