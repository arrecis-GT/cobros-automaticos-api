namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Cobro
    {
        public int CobroId { get; set; }

        public int ClienteId { get; set; }

        public decimal Monto { get; set; } = 0;

        public string Moneda { get; set; } = "QTZ";

        public string Estado { get; set; } = String.Empty;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaProceso { get; set; }

        public TimeOnly HoraProceso { get; set; }

        public string ReferenciaExterna { get; set; } = String.Empty;

        public char Status { get; set; } = 'A';

    }
}
