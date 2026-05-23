using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Infraestructure.Repositories;

namespace CobrosAutomaticosApi.Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        public AuthenticationRepository _repository;
        public async Task<LoginResponse> LogIn(LoginRequest request)
        {

            var UserFind = await _repository.GetUserByUserName(request.UserName);

            if (UserFind == null)
            {
                return new LoginResponse
                {
                    StatusCode = 404,
                    Message = "Usuario no encontrado",
                };
            }

            return new LoginResponse
            {
                UserName = UserFind.UserName,
                Token = "JWT_TOKEN",
                StatusCode = 200,
                Message = "Login successful",
            };
        }   

    }
}
