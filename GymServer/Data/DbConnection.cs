using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GymServer.Data
{
    public interface IDbConnectionn
    {
        public SqlConnection GetConnection { get; }
    }
    public class DbConnection : IDbConnectionn
    {
        IConfiguration Configuration;

        public DbConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SqlConnection GetConnection
        {
            get
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            }
        }
    }
}
