﻿using ContainerSurveyMaui.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;

//using ContainerSurveyMaui.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Services
{
    
    public interface IAuthService
    {
        Task<bool> LoginApi(string username, string password);
        Task<String> GetToken();
        Task<string> GetUserInfo();
        Task<bool> Resetpassword(string username, string oldpassword, string newpassword);
    }

    class AuthService:IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<bool> LoginApi(string username, string password)
        {
            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);
            var email = username;

            string jsonData = JsonSerializer.Serialize(new
            {
                email,
                password
            });
            var requestData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var resp = await _httpClient.PostAsync("api/Api/Login", requestData);
                var response = await resp.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                var token = jsonResponse["Token"].ToString();
                var role = jsonResponse["Role"].ToString();
                var firsttime = jsonResponse["FirsttimeUser"].ToString();

                await SecureStorage.SetAsync("firsttime_user", firsttime);

                JsonObject result =new JsonObject();
                await SecureStorage.SetAsync("jwt_token",token);
                await SecureStorage.SetAsync("Role",role);
                //DisplayAlert("Alert",w, "OK");
                if (resp.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<String> GetToken()
        {
            try
            {
                return await SecureStorage.GetAsync("jwt_token");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> GetRole()
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);
                var response = await _httpClient.GetAsync("/api/Api/RoleInfo");
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> GetUserInfo()
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);
                string token = await GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("/api/Api/UserInfo");
                var strResponse = await response.Content.ReadAsStringAsync();
                //var data = JsonSerializer.Deserialize<List<userDetails>>(strResponse);
                return strResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Resetpassword(string username,string oldpassword, string newpassword)
        {
            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);
            string token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string jsonData = JsonSerializer.Serialize(new
            {
                username,
                oldpassword,
                newpassword
            });
            var requestData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Api/Resetpassword", requestData);
            var strResponse = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;


        }

        public class AuthResult
        {
            public string Token { get; set; }
            public string Role { get; set; }
        }

    }
}
