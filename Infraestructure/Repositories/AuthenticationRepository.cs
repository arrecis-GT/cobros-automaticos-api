using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Domain.Entities;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;
using BCrypt.Net;

namespace CobrosAutomaticosApi.Infraestructure.Repositories
{
    public class AuthenticationRepository: IAuthenticationRespository
    {
        private readonly ConnexionDB _db;
        public AuthenticationRepository(ConnexionDB db)
        {
            _db = db;
        }

        public async Task<Usuario> GetUserByUserName(string UserName)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @"
                SELECT  
                       username, 
                       password 
                FROM Usuario 
                WHERE username = @UserName";


            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName });

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    UserName = reader["username"].ToString(),
                    Password = reader["password"].ToString()
                };
            }

            return null;
        }




    }
}
