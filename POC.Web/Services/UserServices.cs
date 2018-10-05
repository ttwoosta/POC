using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace POC_Web.Services
{
    public class UserServices
    {
        public UserServices()
        {
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<User> users = null;
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync(@"/api/users");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = null;

            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync(@"/api/users/?email=" + email);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
            
        }

        public bool CreateUser(User user)
        {
            return true;
        }
    }
}
