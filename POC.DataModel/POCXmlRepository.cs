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
        public IEnumerable<POCXml> xmls
        {
            get
            {
                DataContext db = POCDB.POCDBContext();

                // get a typed table to run queries
                Table<POCXml> xmlTable = db.GetTable<POCXml>();

                return xmlTable.AsEnumerable();
            }
        }
    }
}
