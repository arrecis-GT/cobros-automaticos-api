using CobrosAutomaticosApi.Domain.Entities;

namespace CobrosAutomaticosApi.Application.DTOs
{
    public record ListarCobroResponse: BaseResponse
    {
        public List<Cobro> Cobros { get; init; } = new List<Cobro>();
    }
}
