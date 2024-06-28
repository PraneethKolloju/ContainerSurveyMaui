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
    public 
    GetPostSevice _getpostservice;
    private ViewImageViewModel viewModel;

     


    public SurveyPage()
    {
        InitializeComponent();
        _getpostservice = new GetPostSevice();
        viewModel = new ViewImageViewModel(); // Initialize viewModel
        //BindingContext = viewModel;
        //GetRes();
    }

    public async void GetRes()
    {
        try
        {
           // Initialize viewModel first
            var res = await _getpostservice.SurveyDetails(1, 4);
            viewModel.VbImage = res;
            BindingContext = viewModel;
        }
        catch (Exception ex)
        {

            throw;
        }

    }


    protected override bool OnBackButtonPressed()
    {

        return true;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int? i = surveyEntry.id;

        var res = await _getpostservice.SurveyDetails(i, 1);


        if (res == null) DisplayAlert("OK", "No Attachment Found", "cancel");
        else
        {
            await Navigation.PushAsync(new DetailsPage(res));
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int? i = surveyEntry.id;

        var res = await _getpostservice.SurveyDetails(i, 2);
        if (res == null) DisplayAlert("OK", "No Attachment Found", "cancel");
        else
        {
            await Navigation.PushAsync(new DetailsPage(res));
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int? i = surveyEntry.id;

        var res = await _getpostservice.SurveyDetails(i, 3);
        if (res == null) DisplayAlert("OK", "No Attachment Found", "cancel");
        else
        {
            await Navigation.PushAsync(new DetailsPage(res));
        }
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var surveyEntry = button.BindingContext as SurveyEntry;
        if (surveyEntry == null) return;
        int? i = surveyEntry.id;

        var res = await _getpostservice.SurveyDetails(i, 4);

        if (res == null)
        {          
            DisplayAlert("OK", "No Attachment Found", "cancel");
        }
        else
        {
            try
                {

                viewModel.VbImage = res;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }

    








}