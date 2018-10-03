using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace POC.DataModel.Services
{
    public class UserServices
    {

        static HttpClient client = new HttpClient();

        public UserServices()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:9810/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<User> users = null;
            HttpResponseMessage response = await client.GetAsync("api//users");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = null;

            HttpResponseMessage response = await client.GetAsync("api//users//?email=" + email);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
            
        }

    }
}
