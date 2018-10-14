using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel
{
    public static class UsersDB
    {

        public static IEnumerable<User> GetUsers()
        {
            Table<User> table = POCDB.POCDBContext().GetTable<User>();
            return table.OrderBy(u => u.Id);
        }

        public static User GetUserById(int Id)
        {
            Table<User> table = POCDB.POCDBContext().GetTable<User>();
            return table.Where(u => u.Id == Id).First();
        }

        public static int CountUserById(int Id)
        {
            Table<User> table = POCDB.POCDBContext().GetTable<User>();
            return table.Where(u => u.Id == Id).Count();
        }

        public static User GetUserByEmail(string email)
        {
            User newUser = new User();

            using (SqlConnection con = POCDB.GetNewSQLConnection())
            {
                var sqlQuery = "Select * from Users where Email='" + email + "'";
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

        public static bool Update(User user)
        {
            DataContext db = POCDB.POCDBContext();
            Table<User> table = db.GetTable<User>();
            User currentUser = table.Where(u => u.Id == user.Id).First();
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            db.SubmitChanges();
            return true;
        }

        public static bool Create(User newUser)
        {
            DataContext db = POCDB.POCDBContext();
            Table<User> table = db.GetTable<User>();
            table.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return true;
        }

        public static bool Delete(int Id)
        {
            SqlConnection con = POCDB.GetNewSQLConnection();

            try
            {
                con.Open();
                // insert statement
                const string UPDATE_STATEMENT = "DELETE FROM Users" +
                    "  WHERE Id=@Id";

                // create insert command
                SqlCommand cmd = new SqlCommand(UPDATE_STATEMENT);
                // add values to command
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Connection = con;

                // execute the command
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                    return true;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
