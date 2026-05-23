namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Cliente
    {
        public int Clinha { get; init; }

        public string Dpi { get; init; } = string.Empty;

        public string Nombre { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string Telefono { get; init; } = string.Empty;

        public char Status { get; init; } = 'A';
    }
}
