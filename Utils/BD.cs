using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Helpers;

namespace Pizzas.API.Controllers
{
    public class basededatos {
        public static SqlConnection GetConnection(){
            SqlConnection db;
            string connectionString;
            //VER ERROR
            connectionString = ConfigurationHelper.GetConfiguration().GetValue<string>("DatabaseSettings: ConnectionString");
            db= new SqlConnection(connectionString);
            return db;
        }

    }
}