using CobrosAutomaticosApi.Domain.Entities;
using CobrosAutomaticosApi.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CobrosAutomaticosApi.Infraestructure.Repositories
{
    public class CobroRepository
    {

        private readonly ConnexionDB _db;

        public CobroRepository(ConnexionDB db)
        {
            _db = db;
        }

        public async Task<bool> CreateCobro(int ClienteId, decimal Monto, string Moneda, string ReferenciaExterna)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @";
            BEGIN TRY
                INSERT INTO Cobro
                (cliente_id, monto, moneda, estado, fecha_creacion, referencia_externa, status)
                VALUES
                (@ClienteId, @Monto, @Moneda, 'PENDIENTE', GETUTCDATE(), @ReferenciaExterna, 'A');
            END TRY
            BEGIN CATCH
                THROW;
            END CATCH";
            

            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = ClienteId });
            command.Parameters.Add(new SqlParameter("@Monto", SqlDbType.Decimal) { Value = Monto });
            command.Parameters.Add(new SqlParameter("@Moneda", SqlDbType.NVarChar) { Value = Moneda });
            command.Parameters.Add(new SqlParameter("@ReferenciaExterna", SqlDbType.NVarChar) { Value = ReferenciaExterna });

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? true : false;

        }

    }
}
