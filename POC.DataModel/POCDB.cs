using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    public static class POCDB
    {
        const string SqlExpConn = "Data Source=MA-TTONG-L;Initial Catalog=POC;Integrated Security=True";

        public static SqlConnection GetNewSQLConnection()
        {
            return new SqlConnection(SqlExpConn);
        }

    }
}
