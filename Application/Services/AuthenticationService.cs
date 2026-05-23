using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Infraestructure.Repositories;

namespace CobrosAutomaticosApi.Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        public AuthenticationRepository _repository;

        public AuthenticationService(AuthenticationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResponse> LogIn(LoginRequest Request)
        {

            var UserFind = await _repository.GetUserByUserName(Request.UserName);

            if (UserFind == null)
            {
                return new LoginResponse
                {
                    StatusCode = 404,
                };
            }

            return new LoginResponse
            {
                UserName = UserFind.UserName,
                Token = "JWT_TOKEN",
                StatusCode = 200,
            };
        }   

    }
}
