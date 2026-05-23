namespace CobrosAutomaticosApi.Application.DTOs
{
    public record LoginResponse : BaseResponse
    {
        public string UserName { get; init; } = string.Empty;

        public string Token { get; init; } = string.Empty;

    }
}
