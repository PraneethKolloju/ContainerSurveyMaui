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
    public string strContainerNo = "";

    public SurveyPage()
    {
        InitializeComponent();
        _getpostservice = new GetPostSevice();
        viewModel = new MainViewModel();
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(500);
        viewModel = new MainViewModel();
        BindingContext = viewModel;
        var firsttime = await SecureStorage.GetAsync("firsttime_user");
        if (firsttime == "1")
        {
            await Task.Delay(3000);
            await DisplayAlert("Alert", "You're a New User, Reset the password", "OK");

            await Navigation.PushAsync(new ResetPwdPage());

        }



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
            await DisplayAlert("Alert", "No Attachment Found", "OK");
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

        

        Preferences.Set("searchedItem", searchedNo);
        Preferences.Set("searchedDateData", searchedDate.ToString());
        viewModel.LoadSurveySearchDataAsync(searchedNo,searchedDate,searchedPort,searchedYard,searchedShippingLine);
    }

    private async void Image_Download(object sender, EventArgs e)
    {
        try
        {
            var button = sender as Button;
            if (button == null) return;

            var surveyEntry = button.BindingContext as SurveyEntry;
            if (surveyEntry == null) return;
            int i = surveyEntry.id.Value;
            strContainerNo = surveyEntry.container_No;
            var res = await _getpostservice.GetImages(i);
            var data = JsonSerializer.Deserialize<List<SurveyDetails>>(res);
            await SaveAttachments(data[0]);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
            return;
        }
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
                    var res = await FileSaver.SaveAsync(strContainerNo+"_Attachment1.jpg", stream, CancellationToken.None);
                    if(res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Attachment 1 not found", "OK");
            }

            if (imageData.attachment_2 != null)
            {

                
                using (var stream = new MemoryStream(imageData.attachment_2))
                {
                    var res = await FileSaver.SaveAsync(strContainerNo + "_Attachment2.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Attachment 2 not found", "OK");
            }

            if (imageData.attachment_3 != null)
            {

                using (var stream = new MemoryStream(imageData.attachment_3))
                {
                    var res = await FileSaver.SaveAsync(strContainerNo + "_Attachment3.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Attachment 3 not found", "OK");
            }

            if (imageData.attachment_4 != null)
            {

                using (var stream = new MemoryStream(imageData.attachment_4))
                {
                    var res = await FileSaver.SaveAsync(strContainerNo + "_Attachment4.jpg", stream, CancellationToken.None);
                    if (res.IsSuccessful)
                    {
                        i += 1;
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Attachment 4 not found", "OK");
            }

            if (i >= 1)
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

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var CurrentPage = (Microsoft.Maui.Controls.Page)Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

        await CurrentPage.FadeOut();

                    await Navigation.PushAsync(new PortDetailsPage());

        await CurrentPage.FadeOut();

    }
}


