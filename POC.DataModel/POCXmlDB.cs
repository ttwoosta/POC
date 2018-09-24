using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POC.DataModel
{
    public static class POCXmlDB
    {
        public static int Count(int pocId)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            return xmlTable.Where(p => p.Id == pocId).Count();
        }

        #region GET
        public static List<POCXml> Get()
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            return (from xml in xmlTable
                    orderby xml.Id
                    select xml).ToList();
        }

        public static POCXml Get(int pocId)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            return (from xml in xmlTable where xml.Id == pocId select xml).First();
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
            cmd.Parameters.AddWithValue("@Id", pXml.Id);
            cmd.Parameters.AddWithValue("@TypeId", pXml.XmlTypeId);
            cmd.Parameters.Add(new SqlParameter("@XmlFile", SqlDbType.Xml) {
                Value = pXml.XmlFile,
            });

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
                "SET Xml_TypeId = @TypeId, XmlFile = @XmlFile " +
                "WHERE Xml_Id = @Id";

            // create insert command
            SqlCommand cmd = new SqlCommand(UPDATE_STATEMENT);

            // add values to command
            cmd.Parameters.AddWithValue("@Id", pXml.Id);
            cmd.Parameters.AddWithValue("@TypeId", pXml.XmlTypeId);
            cmd.Parameters.Add(new SqlParameter("@XmlFile", SqlDbType.Xml)
            {
                Value = pXml.XmlFile,
            });

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
