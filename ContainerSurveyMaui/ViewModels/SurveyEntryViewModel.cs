using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.ViewModels
{

    public class SurveyEntryViewModel:BaseViewModel
    {
        public ObservableCollection<SurveyEntry> SurveyData { get; set; }
        private readonly GetPostSevice _getpostservice;


        public SurveyEntryViewModel()

        {
            _getpostservice= new GetPostSevice();
            SurveyData= new ObservableCollection<SurveyEntry>();   
            LoadDataAsync();

        }

        private async void LoadDataAsync()
        {
            try
            {
                var result = await _getpostservice.GetSurveyData();
                var data = JsonSerializer.Deserialize<List<SurveyEntry>>(result);

                foreach (var i in data)
                {
                    var temp = new SurveyEntry { id = i.id,yard=i.yard, port = i.port, container_No = i.container_No, container_Selection = i.container_Selection,remarks=i.remarks,location=i.location };
                    SurveyData.Add(temp);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



    }
}
