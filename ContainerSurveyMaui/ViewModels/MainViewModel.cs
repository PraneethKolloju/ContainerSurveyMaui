using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private byte[] vbImage;
        private readonly GetPostSevice _getpostservice;

        public ObservableCollection<SurveyEntry> SurveyData { get; set; }

        public byte[] VbImage
        {
            get { return vbImage; }
            set
            {
                if (vbImage != value)
                {
                    vbImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            _getpostservice = new GetPostSevice();
            SurveyData = new ObservableCollection<SurveyEntry>();
            LoadSurveyDataAsync();
        }

        private async void LoadSurveyDataAsync()
        {
            try
            {
                var result = await _getpostservice.GetSurveyData();
                var data = JsonSerializer.Deserialize<List<SurveyEntry>>(result);

                foreach (var item in data)
                {
                    var temp = new SurveyEntry
                    {
                        id = item.id,
                        yard = item.yard,
                        port = item.port,
                        container_No = item.container_No,
                        container_Selection = item.container_Selection,
                        remarks = item.remarks
                    };
                    SurveyData.Add(temp);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
        }

        public async Task LoadImageAsync(int surveyId, int detailType)
        {
            try
            {
                var res = await _getpostservice.SurveyDetails(surveyId, detailType);
                if (res != null)
                {
                    VbImage = res;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
