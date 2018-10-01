using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel.Services
{
    public class UserServices : IUserServices
    {

        public IEnumerable<User> GetUsers()
        {
            List<User> lstuser = new List<User>();
            using (SqlConnection con = POCDB.GetNewSQLConnection())
            {
                var sqlQuery = "Select * from User";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(rdr["Id"]);
                    user.Name = rdr["Name"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Password = rdr["Password"].ToString();

                    lstuser.Add(user);
                }
                con.Close();
            }
            return lstuser;
        }

        public User GetUserByEmail(string email)
        {
            User newUser = new User();
            using (SqlConnection con = POCDB.GetNewSQLConnection())
            {
                var sqlQuery = "Select * from Users where Email='"+email+"'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    newUser.Id = Convert.ToInt32(rdr["Id"]);
                    newUser.Name = rdr["Name"].ToString();
                    newUser.Email = rdr["Email"].ToString();
                    newUser.Password = rdr["Password"].ToString();

                }
                con.Close();
            }
            return newUser;
        }

    }
}
