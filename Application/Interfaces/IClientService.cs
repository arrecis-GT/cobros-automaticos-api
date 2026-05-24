using CobrosAutomaticosApi.Application.DTOs;

namespace CobrosAutomaticosApi.Application.Interfaces
{
    public interface IClientService
    {
        Task<BaseResponse> CrearClient(CreateClientRequest request);
        Task<bool> FindClientByDpi(string Dpi);
    }
}
