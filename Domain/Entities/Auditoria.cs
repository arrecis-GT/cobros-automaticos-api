namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Auditoria
    {
        public int AuditoriaId { get; set; }

        public int UsuarioId { get; set; }

        public string Evento { get; set; } = string.Empty;

        public string EstadoEvento { get; set; } = string.Empty;

        public string ResumenPayload { get; set; } = string.Empty;

        public DateOnly FechaCreacion { get; set; }

        public TimeOnly HoraCreacion { get; set; }

        public char Status { get; set; } = 'A';
    }
}
