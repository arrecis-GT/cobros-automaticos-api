namespace CobrosAutomaticosApi.Application.DTOs
{
    public record BaseResponse
    {
        public string StatusCode { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;
    }
}
