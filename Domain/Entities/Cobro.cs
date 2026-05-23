namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Cobro
    {
        public int CobroId { get; init; }

        public int ClienteId { get; init; }

        public decimal Monto { get; init; } = 0;

        public string Moneda { get; init; } = "QTZ";

        public string Estado { get; init; } = String.Empty;

        public DateTime FechaCreacion { get; init; }

        public DateTime FechaProceso { get; init; }

        public TimeOnly HoraProceso { get; init; }

        public string ReferenciaExterna { get; init; } = String.Empty;

        public char Status { get; init; } = 'A';

    }
}
