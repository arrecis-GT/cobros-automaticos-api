namespace CobrosAutomaticosApi.Domain.Entities
{
    public class Cliente
    {
        public int Clinha { get; set; }

        public string Dpi { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public char Status { get; set; } = 'A';
    }
}
