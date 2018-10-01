using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel.Services
{
   public interface IUserServices
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string username);
    }
}
