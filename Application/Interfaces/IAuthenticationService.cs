using CobrosAutomaticosApi.Application.DTOs;

namespace CobrosAutomaticosApi.Application.Interfaces
{
    public interface IAuthenticationService
    {
       Task<LoginResponse> LogIn(LoginRequest request);
    }
}
