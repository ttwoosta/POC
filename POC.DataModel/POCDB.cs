using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace POC.DataModel
{
    public static class POCDB
    {
        const string SqlExpConn = "Data Source=MA-TTONG-L;Initial Catalog=POC;Integrated Security=True";

        public static SqlConnection GetNewSQLConnection()
        {
            return new SqlConnection(SqlExpConn);
        }

        public static DataContext POCDBContext()
        {
            return new DataContext(SqlExpConn);
        }
    }
}
