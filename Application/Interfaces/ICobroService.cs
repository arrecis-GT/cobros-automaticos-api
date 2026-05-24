using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Domain.Entities;

namespace CobrosAutomaticosApi.Application.Interfaces
{
    public interface ICobroService
    {
        Task<BaseResponse> CrearCobro(CreateCobroRequest request);
        Task<ListarCobroResponse> ListarCobros(int ClienteId, DateOnly? FechaInicio = null, DateOnly? FechaFin = null);
    }
}
