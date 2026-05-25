using CobrosAutomaticosApi.Domain.Entities;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CobrosAutomaticosApi.Infraestructure.Repositories
{
    public class ClienteRespository
    {
        private readonly ConnexionDB _db;

        public ClienteRespository(ConnexionDB db)
        {
            _db = db;
        }

        public async Task<bool> FindClientByDpi(string Dpi)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @"
                SELECT COUNT(1)
                FROM Cliente 
                WHERE dpi = @Dpi";            


            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Dpi", SqlDbType.NVarChar) { Value = Dpi });

            var result = await command.ExecuteScalarAsync();

            return result != null && (int)result > 0;
        }

        public async Task<bool> CrearClient(string Dpi, string Nombre, string Email, string Telefono)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @";
            BEGIN TRY
                INSERT INTO Cliente
                (dpi, nombre, email, telefono, status)
                VALUES
                (@Dpi, @Nombre, @Email, @Telefono, 'A');
            END TRY
            BEGIN CATCH
                THROW;
            END CATCH";


            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@Dpi", SqlDbType.NVarChar) { Value = Dpi });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = Nombre });
            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = Email });
            command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = Telefono });
            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? true : false;

        }


    }
}
