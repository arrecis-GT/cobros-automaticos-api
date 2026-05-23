using CobrosAutomaticosApi.Domain.Entities;

namespace CobrosAutomaticosApi.Application.Interfaces
{
    public interface IAuthenticationRespository
    {
        Task<Usuario> GetUserByUserName(string UserName);
    }
}
