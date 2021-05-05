using SYL_Mobile.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using SYL_Mobile.Models;

namespace SYL_Mobile.Services
{
    public class UserService
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<bool> AddNewUser(NewUserDTO data)
        {
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _client.PostAsync("http://localhost:5000/api/user/", httpContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public async Task<Users> UserLogin(string email, string password)
        {
            try
            {
                string url = string.Format("http://linklocal:5000/api/User?email={0}&password={1}", email, password);
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Users user = JsonConvert.DeserializeObject<Users>(responseBody);
                    return user;
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}
