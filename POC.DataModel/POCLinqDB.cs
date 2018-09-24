using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace POC.DataModel
{
    public static class POCLinqDB
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
        public static void Insert(POCXml xml)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            xmlTable.InsertOnSubmit(xml);

            // submit changes to the Database
            db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public static void Insert(List<POCXml> xmls)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            xmlTable.InsertAllOnSubmit(xmls);

            // submit changes to the Database
            db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
        #endregion
               
        public static void Update(POCXml xml)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            // query for specific customer
            // and change the xml values
            POCXml existing = (from p in xmlTable where p.Id == xml.Id select p).First();
            existing.XmlTypeId = xml.XmlTypeId;
            existing.XmlFile = xml.XmlFile;

            // submitting changes to the Database
            db.SubmitChanges();
        }

        public static void Delete(int xmlId)
        {
            DataContext db = POCDB.POCDBContext();

            // get a typed table to run queries
            Table<POCXml> xmlTable = db.GetTable<POCXml>();

            // query for specific ConversionXML
            POCXml existing = (from p in xmlTable where p.Id == xmlId select p).First();
            // mark ConversionXMl row for deletion from the database
            xmlTable.DeleteOnSubmit(existing);

            // submitting changes to the Database
            db.SubmitChanges();
        }
    }
}
