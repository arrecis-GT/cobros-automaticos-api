using CobrosAutomaticosApi.Application.DTOs;

namespace CobrosAutomaticosApi.Application.Interfaces
{
    public interface ICobroService
    {
        Task<BaseResponse> CreateCobro(CreateCobroRequest request);
    }
}
