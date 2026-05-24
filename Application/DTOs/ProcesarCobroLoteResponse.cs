namespace CobrosAutomaticosApi.Application.DTOs
{
    public record ProcesarCobroLoteResponse: BaseResponse
    {
        public int CobrosProcesados { get; init; }
    }
}
