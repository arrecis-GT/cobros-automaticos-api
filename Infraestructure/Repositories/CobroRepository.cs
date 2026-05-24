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

        public async Task<bool> CrearCobro(int ClienteId, decimal Monto, string Moneda, string ReferenciaExterna)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @";
            BEGIN TRY
                INSERT INTO Cobro
                (cliente_id, monto, moneda, estado, fecha_creacion, fecha_proceso, hora_proceso, referencia_externa, status)
                VALUES
                (@ClienteId, @Monto, @Moneda, 'PENDIENTE', GETDATE(), '1900-01-01', '00:00:00', @ReferenciaExterna, 'A');
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

        public async Task<List<Cobro>> ListarCobros(int ClienteId, DateOnly? FechaInicio = null, DateOnly? FechaFin = null)
        {
            using var connection = await _db.GetConnectionAsync();

            const string query = @";
            BEGIN TRY
                SELECT * FROM Cobro 
                WHERE cliente_id = @ClienteId
                AND (@FechaInicio IS NULL OR CAST(fecha_creacion AS DATE) >= @FechaInicio)
                AND (@FechaFin IS NULL OR CAST(fecha_creacion AS DATE) <= @FechaFin)
            END TRY
            BEGIN CATCH
                THROW;
            END CATCH";


            using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = ClienteId });
            command.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.Date) { Value = (object?)FechaInicio ?? DBNull.Value });
            command.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.Date) { Value = (object?)FechaFin ?? DBNull.Value });

            var reader = await command.ExecuteReaderAsync();
            var cobros = new List<Cobro>();

            while (await reader.ReadAsync())
            {
                cobros.Add(new Cobro
                {
                    CobroId = reader.GetInt32("cobro_id"),
                    ClienteId = reader.GetInt32("cliente_id"),
                    Monto = reader.GetDecimal("monto"),
                    Moneda = reader.GetString("moneda"),
                    Estado = reader.GetString("estado"),
                    FechaCreacion = DateOnly.FromDateTime(reader.GetDateTime("fecha_creacion")),
                    FechaProceso = reader.GetDateTime("fecha_proceso") != DateTime.MinValue ? DateOnly.FromDateTime(reader.GetDateTime("fecha_proceso")) : null,
                    HoraProceso = reader["hora_proceso"] != DBNull.Value? TimeOnly.FromTimeSpan((TimeSpan)reader["hora_proceso"]): null,
                    ReferenciaExterna = reader.GetString("referencia_externa"),
                    Status = Convert.ToChar(reader["status"])
                }); 
            }

            return cobros;
        }


        public async Task<bool> ProcesarCobroIndividual(int UsuarioId, int CobroId)
        {

            string payload = $"{{{CobroId}}}";

            using var connection = await _db.GetConnectionAsync();
            using var command = new SqlCommand("SP_ProcesarCobroIndividual", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = UsuarioId });
            command.Parameters.Add(new SqlParameter("@CobroId", SqlDbType.Int) { Value = CobroId });
            command.Parameters.Add(new SqlParameter("@Payload", SqlDbType.NVarChar) { Value = payload });

            var outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParam);

            await command.ExecuteNonQueryAsync();

            return (bool)outputParam.Value;
        }


        public async Task<int> ProcesarCobroLote(int UsuarioId, int[] CobroIds)
        {

            var ListaCobros = new DataTable();
            ListaCobros.Columns.Add("cobro_id", typeof(int));

            // 2. Llenamos la tabla con los IDs de nuestra lista
            foreach (var id in CobroIds)
            {
                ListaCobros.Rows.Add(id);
            }

            using var connection = await _db.GetConnectionAsync();
            using var command = new SqlCommand("SP_ProcesarCobroLote", connection);
            command.CommandType = CommandType.StoredProcedure;

            var tvpParam = new SqlParameter("@LoteCobros", SqlDbType.Structured)
            {
                Value = ListaCobros,
                TypeName = "ListaCobros"
            };
            command.Parameters.Add(tvpParam);

            // Agregamos el resto de los parámetros
            command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = UsuarioId });
            command.Parameters.Add(new SqlParameter("@Payload", SqlDbType.NVarChar) { Value = string.Join(",", CobroIds) });

            var outputParam = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(outputParam);

            var result = await command.ExecuteNonQueryAsync();

            return (int)outputParam.Value;
        }


    }
}

