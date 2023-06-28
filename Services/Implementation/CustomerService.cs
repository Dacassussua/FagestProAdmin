using FagestProAdmin.Models;
using FagestProAdmin.Models.Customer;
using FagestProAdmin.Models.General;
using FagestProAdmin.Services.Interfaces.ICustomer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FagestProAdmin.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private HttpClient _httpClient;
        private readonly string BasePath = $"api/Customer";
        public CustomerService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(AppGeneral.PathLicenceUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (AppGeneral.loginResponse ?? new LoginResponse()).token);
        }

        public async Task<bool> CreateAsync(object obj)
        {
            var model = obj as CustomerViewModel;
            var data = JsonSerializer.Serialize(model);
            var stringcontent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BasePath}/Create", stringcontent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<Response>(result);
            if ((content ?? new Response()).statusCode == 201)
                return true;

            return false;
        }
        public async Task<bool> UpdateAsync(object obj)
        {
            var model = obj as CustomerViewModel;
            var data = JsonSerializer.Serialize(model);
            var contentstring = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BasePath}/Update", contentstring);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<Response>(result);
            if ((content ?? new Response()).statusCode == 200)
                return true;

            return false;
        }
        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BasePath}/GetAll");
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                    return new List<CustomerViewModel>();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<CustomerViewModel>>(content);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CustomerViewModel> GetByIdAsync(object EntityId)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/GetByCustomerId/{string.Format("{0}", EntityId)}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return new CustomerViewModel();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerViewModel>(content);
        }

        public async Task<bool> RemoveAsync(object EntityId)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/Remove/{string.Format("{0}", EntityId)}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return false;


            var result = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<Response>(result);
            if ((content ?? new Response()).statusCode == 200)
                return true;

            return false;
        }


    }
}
