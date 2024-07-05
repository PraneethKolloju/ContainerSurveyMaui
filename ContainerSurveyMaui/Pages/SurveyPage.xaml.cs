using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System.Collections;
using System.Text.Json;
using System.Drawing;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using SkiaSharp;
using ContainerSurveyMaui.ViewModels;

namespace ContainerSurveyMaui.Pages;
public partial class SurveyPage : ContentPage
{
    public GetPostSevice _getpostservice;
    private MainViewModel viewModel;

    public SurveyPage()
    {
        InitializeComponent();
        _getpostservice = new GetPostSevice();
        viewModel = new MainViewModel(); // Initialize viewModel
        BindingContext = viewModel; // Set the BindingContext here
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

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        var searchedNo = ContainerNo.Text;

    }
}
