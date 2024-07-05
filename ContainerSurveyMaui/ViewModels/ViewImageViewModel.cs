using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using Xamarin.Essentials;

namespace ContainerSurveyMaui.ViewModels
{
    public class ViewImageViewModel : BaseViewModel
    {
        public ObservableCollection<SurveyDetails> ImageData { get; set; }
        private readonly GetPostSevice _getpostservice;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ViewImageViewModel()
        {
            _getpostservice = new GetPostSevice();
            ImageData = new ObservableCollection<SurveyDetails>();
            IsLoading = true; 
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                int id = Microsoft.Maui.Storage.Preferences.Get("Id", 0);
                var res = await _getpostservice.GetImages(id);
                var data = JsonSerializer.Deserialize<List<SurveyDetails>>(res);

                foreach (var i in data)
                {
                    var temp = new SurveyDetails
                    {
                        attachment_1 = i.attachment_1,
                        attachment_2 = i.attachment_2,
                        attachment_3 = i.attachment_3,
                        attachment_4 = i.attachment_4,
                        id = i.id
                    };
                    ImageData.Add(temp);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (logging, error messages, etc.)
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Reset loading state
            }
        }
    }
}
