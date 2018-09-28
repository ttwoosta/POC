using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    [Table(Name = "dbo.User")]
    public class User
    {
        
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int Id { get; set; }

            [Column(UpdateCheck = UpdateCheck.WhenChanged)]
            public string Name { get; set; }

            [Column(UpdateCheck = UpdateCheck.WhenChanged)]
            public string Email { get; set; }

            [Column(UpdateCheck = UpdateCheck.WhenChanged)]
            public string Password { get; set; }


    }
}
