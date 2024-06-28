namespace ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Constants;
using ContainerSurveyMaui.Services;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;

public partial class ViewDataPage : ContentPage
{
    private readonly AuthService _authService;

    public ViewDataPage()
    {
        InitializeComponent();
        _authService = new AuthService();

    }
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());

    }
    
    private async void TableView_Loaded(object sender, EventArgs e)
    {
        var data =await  _authService.GetUserInfo();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage()); 
    }
   
}