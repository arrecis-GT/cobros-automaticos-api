using BCrypt.Net;
using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Domain.Entities;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CobrosAutomaticosApi.Infraestructure.Repositories
{
    public class AuthenticationRepository
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

        public async Task<SesionResponse> CheckExistSession(string UserName)
        {
            
            using var connection = await _db.GetConnectionAsync();
            const string query = @"
                SELECT
                    U.username,
	                S.fecha_creacion,
	                S.hora_creacion,
	                S.ultima_conexion
                FROM Sesion S
                INNER JOIN Usuario U ON S.usuario_id = U.usuario_id
                WHERE U.username = @UserName
                AND   S.status = 'A'";

            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName });

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new SesionResponse
                {
                    UserName = reader["username"].ToString(),
                    FechaCreacion = DateOnly.FromDateTime((DateTime)reader["fecha_creacion"]),
                    HoraCreacion = TimeOnly.FromTimeSpan((TimeSpan)reader["hora_creacion"]),
                    UltimaConexion = TimeOnly.FromTimeSpan((TimeSpan)reader["ultima_conexion"])
                };  
            }

            return null;
        }


    }
}
