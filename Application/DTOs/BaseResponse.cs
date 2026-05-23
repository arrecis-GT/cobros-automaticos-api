namespace CobrosAutomaticosApi.Application.DTOs
{
    public record BaseResponse
    {
        public int StatusCode { get; init; };

        public string Message { get; init; } = string.Empty;
    }
}
