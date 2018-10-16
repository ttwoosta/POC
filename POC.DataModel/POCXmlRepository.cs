using POC.DataModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    public class POCXmlRepository : IXmlRepository
    {
        private DataContext _db;

        public POCXmlRepository()
        {
            _db = POCDB.POCDBContext();
        }

        public IEnumerable<POCXml> xmls
        {
            get
            {
                // get a typed table to run queries
                Table<POCXml> xmlTable = _db.GetTable<POCXml>();

                return xmlTable.AsEnumerable();
            }
        }
    }
}
