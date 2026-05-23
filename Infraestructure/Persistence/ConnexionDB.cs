using Microsoft.Data.SqlClient;

namespace CobrosAutomaticosApi.Infraestructure.Persistence
{
    public class ConnexionDB
    {
        private readonly string _connectionString;        

        public ConnexionDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Local");
        }

        public async Task<SqlConnection> GetConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al conectar a la base de datos.");
                throw;
            }

        }

    }
}
