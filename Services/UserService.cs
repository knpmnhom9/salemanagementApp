using salemanagementApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SaleManagementApp.Services
{
    public class UserService
    {
        private readonly HttpClient _client;

        public UserService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:8000/api/");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _client.GetFromJsonAsync<List<User>>("users");
        }

        public async Task AddUserAsync(User user)
        {
            await _client.PostAsJsonAsync("users", user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _client.PutAsJsonAsync($"users/{user.Id}", user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _client.DeleteAsync($"users/{id}");
        }
    }
}
