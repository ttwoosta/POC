using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    public static class POCConXmlDB
    {
        const string kId = "Xml_Id";
        const string kTypeId = "Xml_TypeId";
        const string kXmlFile = "XmlFile";

        #region GET
        private static List<POCXml> Select(string selectStatement, SqlConnection sqlConnection)
        {
            // return object
            List<POCXml> pxmls = new List<POCXml>();

            // create a Select command
            SqlCommand sqlCommand = new SqlCommand(selectStatement, sqlConnection);

            // read sql data
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    POCXml xml = new POCXml();
                    xml.Xml_id = int.Parse(reader[kId].ToString());
                    xml.Xml_TypeId = int.Parse(reader[kTypeId].ToString());
                    xml.XmlFile = reader[kXmlFile].ToString();

                    // add to returned list
                    pxmls.Add(xml);
                }
            }

            // return the result
            return pxmls;
        }

        public static List<POCXml> GetAll()
        {
            const string SELECT_ALL = "SELECT * FROM ConversionXml";

            // request a new connection
            SqlConnection conn = POCDB.GetNewSQLConnection();

            try
            {
                conn.Open();
                return Select(SELECT_ALL, conn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static POCXml Get(int pocId)
        {
            string SELECT_STATEMENT = "SELECT * FROM ConversionXml " + 
                "WHERE Xml_Id = " + pocId.ToString();

            // request a new connection
            SqlConnection conn = POCDB.GetNewSQLConnection();

            try
            {
                conn.Open();
                return Select(SELECT_STATEMENT, conn).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion

        #region INSERT
        private static SqlCommand CreateInsertCommand(POCXml pXml)
        {
            // insert statement
            const string INSERT_STATEMENT = "INSERT INTO ConversionXml " + 
                "(Xml_Id, Xml_TypeId, XmlFile) VALUES " + 
                "(@Id, @TypeId, @XmlFile)";

            // create insert command
            SqlCommand cmd = new SqlCommand(INSERT_STATEMENT);

            // add values to command
            cmd.Parameters.AddWithValue("@Id", pXml.Xml_id);
            cmd.Parameters.AddWithValue("@TypeId", pXml.Xml_TypeId);
            cmd.Parameters.AddWithValue("@XmlFile", pXml.XmlFile);

            // return command
            return cmd;
        }

        public static bool Insert(List<POCXml> pxmls)
        {
            // get a new connection
            SqlConnection conn = POCDB.GetNewSQLConnection();

            try
            {
                conn.Open();

                foreach (POCXml xml in pxmls)
                {
                    // create insert command
                    SqlCommand insertCmd = CreateInsertCommand(xml);
                    insertCmd.Connection = conn;

                    // execute the command
                    int result = insertCmd.ExecuteNonQuery();

                    // if the command couldn't insert new record, 
                    // stop the process immediately
                    if (result != 1)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool Insert(POCXml pxml)
        {
            return Insert(new List<POCXml>() { pxml });
        }
        #endregion

        #region UPDATE
        public static SqlCommand CreateUpdateCommand(POCXml pXml)
        {
            // update statement
            const string UPDATE_STATEMENT = "UPDATE ConversionXml " +
                "SET (Xml_TypeId = @TypeId, XmlFile = @XmlFile) " +
                "WHERE Xml_Id = @Id";

            // create insert command
            SqlCommand cmd = new SqlCommand(UPDATE_STATEMENT);

            // add values to command
            cmd.Parameters.AddWithValue("@Id", pXml.Xml_id);
            cmd.Parameters.AddWithValue("@TypeId", pXml.Xml_TypeId);
            cmd.Parameters.AddWithValue("@XmlFile", pXml.XmlFile);

            // return command
            return cmd;
        }

        public static bool Update(List<POCXml> pxmls)
        {
            // get a new connection
            SqlConnection conn = POCDB.GetNewSQLConnection();

            try
            {
                conn.Open();

                foreach (POCXml xml in pxmls)
                {
                    // create update command
                    SqlCommand updateCmd = CreateUpdateCommand(xml);
                    updateCmd.Connection = conn;

                    // execute the command
                    int result = updateCmd.ExecuteNonQuery();

                    // if the command couldn't update record, 
                    // stop the process immediately
                    if (result != 1)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool Update(POCXml pxml)
        {
            return Update(new List<POCXml>() { pxml });
        }
        #endregion
    }
}
