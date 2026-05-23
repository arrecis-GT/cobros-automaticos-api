namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Auditoria
    {
        public int AuditoriaId { get; init; }

        public int UsuarioId { get; init; }

        public string Evento { get; init; } = string.Empty;

        public string EstadoEvento { get; init; } = string.Empty;

        public string ResumenPayload { get; init; } = string.Empty;

        public DateOnly FechaCreacion { get; init; }

        public TimeOnly HoraCreacion { get; init; }

        public char Status { get; init; } = 'A';
    }
}
