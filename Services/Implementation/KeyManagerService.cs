using FagestProAdmin.Models;
using FagestProAdmin.Models.General;
using FagestProAdmin.Models.Licences;
using FagestProAdmin.Services.Interfaces.ILicence;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FagestKeyGenerator.WebAPP.Services.Implementation
{
    public class KeyManagerService : IKeyManager
    {
        private HttpClient _client;
        private string BasePath = "api/KeyManager";
        public KeyManagerService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(AppGeneral.PathLicenceUrl);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppGeneral.loginResponse.token);
        }
        public async Task<bool> CreateAsync(object obj)
        {
            var token = AppGeneral.loginResponse.token;
            var model = obj as NewKeyManagerViewModel;
            var data = JsonSerializer.Serialize(model);
            var stringContent = new StringContent(data,Encoding.UTF8,"application/json");
            var response = await _client.PostAsync($"{BasePath}/Create", stringContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await  response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(content);
            if (result.statusCode == 201)
                return true;

            return false;
        }

        public async Task<IEnumerable<KeyManagerResult>> GetAllAsync()
        {
            var response = await _client.GetAsync($"{BasePath}/GetAll");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return new List<KeyManagerResult>();

            var content = await response.Content.ReadAsStringAsync();
            var result= JsonSerializer.Deserialize<IEnumerable<KeyManagerResult>>(content);

            return result;
        }

        public Task<KeyManagerResult> GetByIdAsync(object EntityId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveAsync(object EntityId)
        {
            var response = await _client.DeleteAsync($"{BasePath}/Remove/{EntityId}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(content);
            if (result.statusCode == 200)
                return true;

            return false;

        }

        public Task<bool> UpdateAsync(object obj)
        {
            var model = obj as KeyManagerViewModel;
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<KeyManagerResult>> GetEndThisMonthAsync()
        {
            var currentDate = DateTime.Now.Date;
            var response = await _client.GetAsync($"{BasePath}/EndThisMonth/{currentDate.Year}/{currentDate.Month}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return new List<KeyManagerResult>();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<KeyManagerResult>>(content);

            return result;
        }
        public async Task<IEnumerable<KeyManagerResult>> GetCreateThisMonthAsync()
        {
            var currentDate = DateTime.Now.Date;
            var response = await _client.GetAsync($"{BasePath}/CreatedThisMonth/{currentDate.Year}/{currentDate.Month}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return new List<KeyManagerResult>();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<KeyManagerResult>>(content);

            return result;
        }
    }
}
