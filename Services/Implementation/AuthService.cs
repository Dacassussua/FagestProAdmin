using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Cors;
using FagestProAdmin.Services.Interfaces.Login;
using FagestProAdmin.Models.General;
using FagestProAdmin.Models;

namespace FagestKeyGenerator.WebAPP.Services.Implementation
{
    [EnableCors("_myAllowSpecificOrigins")]
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(AppGeneral.PathIdentityUrl);
        }

        public async Task<CurrentUser> CurrentUserInfo()
        {
            AppGeneral.CurrentUserModel = new UserModel();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (AppGeneral.loginResponse ?? new LoginResponse()).token);

            var response = await _httpClient.PostAsync($"api/UserManager/UserByCode/{(AppGeneral.loginResponse ?? new LoginResponse()).activationCode}", null);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return new CurrentUser();
            var content = await response.Content.ReadAsStringAsync();
            AppGeneral.CurrentUserModel = JsonSerializer.Deserialize<UserModel>(content);
            var result = new CurrentUser
            {
                IsAuthenticated = true,
                UserName = AppGeneral.CurrentUserModel.userName,
                Claims = new Dictionary<string, string>() { { "", "" } }
            };
            return result;
        }


        public async Task Login(LoginRequest loginRequest)
        {
            var data = JsonSerializer.Serialize(new LoginViewModel
            {
                Password = loginRequest.Password,
                UserName = loginRequest.UserName
            });
            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");



                var response = await _httpClient.PostAsync("api/AcountManager/UserLogin", content);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception(await response.Content.ReadAsStringAsync());

                var constent = await response.Content.ReadAsStringAsync();
                AppGeneral.loginResponse = JsonSerializer.Deserialize<LoginResponse>(constent);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/auth/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

    }
}
