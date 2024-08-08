using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System.Collections;
using System.Text.Json;
using System.Drawing;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using SkiaSharp;
using ContainerSurveyMaui.ViewModels;
using CommunityToolkit.Maui.Storage;

namespace ContainerSurveyMaui.Pages;
public partial class SurveyPage : ContentPage
{
    public GetPostSevice _getpostservice;
    private MainViewModel viewModel;

    public SurveyPage()
    {
        InitializeComponent();
        _getpostservice = new GetPostSevice();
        viewModel = new MainViewModel();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel = new MainViewModel();
        BindingContext = viewModel;

    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }



    private async void Button_Clicked_4(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int i = surveyEntry.id.Value;

        if (i == null)
        {
            await DisplayAlert("OK", "No Attachment Found", "cancel");
        }
        else
        {
            Navigation.PushAsync(new DetailsPage(i));
            //await Navigation.PushAsync(new DetailsPage(res));
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        string i = surveyEntry.location;
        await Navigation.PushAsync(new MapsPage(i));
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var searchedNo = ContainerNo.Text;
        var searchedDate = DateSelected.Date.ToString();
        var searchedYard = Yard.Text;
        var searchedPort = Port.Text;
        var searchedShippingLine= ShippingLine.Text;

        if (searchedNo == null)
        {
            await DisplayAlert("Warning", "Select Container No", "OK");
            return;
        }
        if (searchedDate == null)
        {
            await DisplayAlert("Warning", "Select Date", "OK");
            return;
        }

        Preferences.Set("searchedItem", searchedNo);
        Preferences.Set("searchedDateData", searchedDate.ToString());
        viewModel.LoadSurveySearchDataAsync(searchedNo,searchedDate,searchedPort,searchedYard,searchedShippingLine);
    }

    private async void Image_Download(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int i = surveyEntry.id.Value;
        var res = await _getpostservice.GetImages(i);
        var data = JsonSerializer.Deserialize<List<SurveyDetails>>(res);
        await SaveAttachments(data[0]);

    }

    private async Task SaveAttachments(SurveyDetails imageData)
    {
        try
        {
            int i = 0;
            //string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //directoryPath = Path.Combine(directoryPath, "DownloadedImages");

            //Directory.CreateDirectory(directoryPath);

            if (imageData.attachment_1 != null)
            {
                
               
                using (var stream=new MemoryStream(imageData.attachment_1))
                {
                    var res = await FileSaver.SaveAsync("Attachment1.jpg", stream, CancellationToken.None);
                    if(res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            if (imageData.attachment_2 != null)
            {

                
                using (var stream = new MemoryStream(imageData.attachment_2))
                {
                    var res = await FileSaver.SaveAsync("Attachment2.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            if (imageData.attachment_3 != null)
            {

                using (var stream = new MemoryStream(imageData.attachment_3))
                {
                    var res = await FileSaver.SaveAsync("Attachment3.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            if (imageData.attachment_4 != null)
            {

                using (var stream = new MemoryStream(imageData.attachment_4))
                {
                    var res = await FileSaver.SaveAsync("Attachment4.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }

            if (i == 4)
            {
                await DisplayAlert("Success", "Images downloaded successfully.", "OK");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to download images: {ex.Message}", "OK");
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        SearchFilters.IsVisible = !SearchFilters.IsVisible;
        if (SearchFilters.IsVisible)
        {
            showhidefitlers.Text = "Hide Filters";
        }
        else
        {
            showhidefitlers.Text = "Show Filters";
        }
    }
}


