using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Services
{

    public interface IGetPostService
    {
        
        Task<bool> SurveyEntry(SurveyEntry Data);
        Task<string> GetMasterData(string? name);

        Task<string> NewPortEntry(NewPortEntry NewPortData);



    }
    public class NewPortEntry
    {
        public string Port { get; set; }
        public string Yard { get; set; }
        public string ShippingLine { get; set; }
        public string loggedInName { get; set; }

    }
    public class GetPostSevice :IGetPostService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _auth;
        public GetPostSevice()
        {
            _httpClient = new HttpClient();
            _auth = new AuthService();  
        }

        public async Task<bool> SurveyEntry(SurveyEntry Data)
        {
            try
            {
                var fulldata = new MultipartFormDataContent();
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                SurveyEntry data = new SurveyEntry
                {
                    port = Data.port,
                    yard = Data.yard,
                    shipping_line = Data.shipping_line,
                    container_No = Data.container_No,
                    container_Selection = Data.container_Selection,
                    remarks = Data.remarks,
                    attachment_1 = Data.attachment_1,
                    attachment_2 = Data.attachment_2,
                    attachment_3 = Data.attachment_3,
                    attachment_4 = Data.attachment_4,
                    location = Data.location,
                };

                //var token =await _auth.GetToken();
                //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync("/api/Api/SurveyEntry",data);
                response.EnsureSuccessStatusCode();
                 return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetSurveyData()
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.GetAsync($"/api/Api/SurveyEntry");
                var data =  response.Content.ReadAsStringAsync().Result;
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Byte[]> SurveyDetails(int? id,int atch)
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"/api/Api/SurveyEntryDetails?id={id}&atch={atch}");
                var data = response.Content.ReadAsStringAsync().Result;
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                return byteArray;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> GetImages(int? id)
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                List<Byte[]> imageresult = new List<byte[]>();

                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"/api/Api/Images?id={id}");
                var data = response.Content.ReadAsStringAsync().Result;
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> GetTopSurveyData()
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"/api/Api/GetTopSurveyData");
                var data = response.Content.ReadAsStringAsync().Result;
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> GetSurveyDataOnSearch(string name,string Date,string port,string yard,string shippingline)
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);
                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"/api/Api/SurveyEntryOnSearch?ContainerNo={name}&Date={Date}&port={port}&yard={yard}&shippingLine={shippingline}");
                var data = response.Content.ReadAsStringAsync().Result;
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> GetMasterData(string? name)
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

                var token = await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var repsonse = await _httpClient.GetAsync($"/api/Api/GetMaster?portName={name}");

                var data = repsonse.Content.ReadAsStringAsync().Result;

                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> NewPortEntry(NewPortEntry NewPortData)
        {
            if(_httpClient.BaseAddress== null)
                _httpClient.BaseAddress= new Uri(Constants.Constants.BaseUrl);  


            string jsonData= System.Text.Json.JsonSerializer.Serialize(new {
                NewPortData.Port,
                NewPortData.Yard,
                NewPortData.ShippingLine,
                NewPortData.loggedInName,
            });

            var requestData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var token= await _auth.GetToken();  
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("/api/Api/NewPortDetailsEntry", requestData);

            var strResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            return strResponse;

        }

    }
}
