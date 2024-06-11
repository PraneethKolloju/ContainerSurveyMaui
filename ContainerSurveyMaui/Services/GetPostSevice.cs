using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
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

                SurveyEntry data= new SurveyEntry{
                    Port= Data.Port,
                    Yard= Data.Yard,
                    Shipping_line= Data.Shipping_line,
                    Container_No= Data.Container_No,
                    Container_Selection= Data.Container_Selection,
                    Remarks= Data.Remarks,
                    Attachment_1= Data.Attachment_1,
                    Attachment_2= Data.Attachment_2,
                    Attachment_3= Data.Attachment_3,
                    Attachment_4= Data.Attachment_4,
                    Location= Data.Location,
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Api/SurveyEntry",data);
                response.EnsureSuccessStatusCode();
                 return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
