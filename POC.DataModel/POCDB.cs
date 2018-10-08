using System.Data.SqlClient;
using System.Data.Linq;
using System.Configuration;

namespace POC.DataModel
{
    public static class POCDB
    {
        const string SqlExpConn = "Data Source=MA-TTONG-L;Initial Catalog=POC;Integrated Security=True";

        public static string SqlConnString()
        {
            ConnectionStringSettings connSett = 
                ConfigurationManager.ConnectionStrings["ReleaseSQLServer"];
            if (connSett == null)
                return SqlExpConn;

            return connSett.ConnectionString;
        }

        public static SqlConnection GetNewSQLConnection()
        {
            return new SqlConnection(SqlConnString());
        }

        public static DataContext POCDBContext()
        {
            return new DataContext(SqlConnString());
        }
    }
}
