namespace CobrosAutomaticosApi.Application.DTOs
{
    public record SesionResponse
    {
        public string UserName { get; init; } = string.Empty;
        public DateOnly FechaCreacion { get; init; }
        public TimeOnly HoraCreacion { get; init; }
        public TimeOnly UltimaConexion { get; init; }
    }
}
