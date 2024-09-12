using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly GetPostSevice _getpostservice;
        private SurveyEntry _selectedSurveyEntry;

        public ObservableCollection<SurveyEntry> SurveyData { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isSearchLoading;
        public bool isSearchLoading
        {
            get => _isSearchLoading;
            set => SetProperty(ref _isSearchLoading, value);
        }

        public SurveyEntry SelectedSurveyEntry
        {
            get => _selectedSurveyEntry;
            set
            {
                _selectedSurveyEntry = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _getpostservice = new GetPostSevice();
            SurveyData = new ObservableCollection<SurveyEntry>();
            IsLoading = true;
            string item = Preferences.Get("ContainerNo","None");
            LoadSurveyDataAsync();
        }

        public async void LoadSurveyDataAsync()
        {
            try
            {
                var result = await _getpostservice.GetTopSurveyData();
                var data = JsonSerializer.Deserialize<List<SurveyEntry>>(result);

                foreach (var item in data)
                {
                    DateTime createdOndt = DateTime.Parse(item.createdOn);
                    SurveyData.Add(new SurveyEntry
                    {
                        id = item.id,
                        yard = item.yard,
                        port = item.port,
                        shipping_line = item.shipping_line,
                        container_No = item.container_No,
                        container_Selection = item.container_Selection,
                        remarks = item.remarks,
                        location=item.location,
                        attachment_1 = item.attachment_1,
                        attachment_2 = item.attachment_2,
                        attachment_3 = item.attachment_3,
                        attachment_4 = item.attachment_4,
                         createdOn = createdOndt.ToString("dd-MM-yyyy"),
                    });

                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            finally
            {
                IsLoading=false;

            }
        }

        public async void LoadSurveySearchDataAsync(string CNo,string CDate,string CPort,string CYard,string CShippingLine)
        {
            IsLoading = true;
            try
            {
                var result = await _getpostservice.GetSurveyDataOnSearch(CNo,CDate,CPort,CYard,CShippingLine);
                var data = JsonSerializer.Deserialize<List<SurveyEntry>>(result);
                SurveyData.Clear();

                foreach (var item in data)
                {
                    SurveyData.Add(new SurveyEntry
                    {
                        id = item.id,
                        yard = item.yard,
                        port = item.port,
                        shipping_line=item.shipping_line,   
                        container_No = item.container_No,
                        container_Selection = item.container_Selection,
                        remarks = item.remarks,
                        location = item.location,
                        attachment_1 = item.attachment_1,
                        attachment_2 = item.attachment_2,
                        attachment_3 = item.attachment_3,
                        attachment_4 = item.attachment_4,

                    });

                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            finally
            {
                IsLoading = false;

            }
        }


        public async Task LoadAttachmentAsync(int surveyId, int attachmentNumber)
        {
            try
            {
                var result = await _getpostservice.SurveyDetails(surveyId,attachmentNumber);
                if (result != null && SelectedSurveyEntry != null)
                {
                    switch (attachmentNumber)
                    {
                        case 1:
                            SelectedSurveyEntry.attachment_1 = result;
                            break;
                        case 2:
                            SelectedSurveyEntry.attachment_2 = result;
                            break;
                        case 3:
                            SelectedSurveyEntry.attachment_3 = result;
                            break;
                        case 4:
                            SelectedSurveyEntry.attachment_4 = result;
                            break;
                    }

                    // Notify that SelectedSurveyEntry has changed
                    OnPropertyChanged(nameof(SelectedSurveyEntry));
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
