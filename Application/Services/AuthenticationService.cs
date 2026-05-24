using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Infraestructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CobrosAutomaticosApi.Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly AuthenticationRepository _repository;
        private readonly IConfiguration _config;

        public AuthenticationService(AuthenticationRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        private string GenerarJwt(string userName)
        {
            string secretKey = _config["JwtConfig:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, userName)]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(descriptor));
        }

        public async Task<LoginResponse> LogIn(LoginRequest Request)
        {

            var UserFind = await _repository.GetUserByUserName(Request.UserName);

            if (UserFind == null)
            {
                return new LoginResponse
                {
                    StatusCode = 401,
                };
            }


            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(Request.Password, UserFind.Password);

            if (!isPasswordValid)
            {
                return new LoginResponse
                {
                    StatusCode = 401,
                };
            }

            // To Do: Falta agregar funcionalidad de la sesión del usuario.

            return new LoginResponse
            {
                UserName = UserFind.UserName,
                Token = GenerarJwt(UserFind.UserName),
                StatusCode = 200,
            };
        }   

    }
}
