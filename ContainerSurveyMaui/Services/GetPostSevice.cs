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

    }
    public class GetPostSevice :IGetPostService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _auth;
        public GetPostSevice()
        {
            _httpClient = new HttpClient();
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

                var token =await _auth.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

                var response = await _httpClient.GetAsync("/api/Api/SurveyEntry");
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
    }
}
