namespace ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Constants;
using ContainerSurveyMaui.Services;
using ContainerSurveyMaui.ViewModels;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;

public partial class ViewDataPage : ContentPage
{
    private readonly AuthService _authService;
    private UserDataViewModel viewModel;

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(500);
        viewModel = new UserDataViewModel();
        BindingContext = viewModel;

    }
    public ViewDataPage()
    {
        InitializeComponent();
        _authService = new AuthService();
        viewModel = new UserDataViewModel();
        BindingContext = viewModel;
    }
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
    
    private async void TableView_Loaded(object sender, EventArgs e)
    {
        var data =await  _authService.GetUserInfo(null,null);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage()); 
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            var userName = username.Text;
            var userRole= role.Text;    
            viewModel.LoadOnSearch(userName,userRole);
            
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void showhidefitlers_Clicked(object sender, EventArgs e)
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

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var userEntry = button.BindingContext as User;
        if (userEntry == null) return;
        int resetId = userEntry.userId;
        var isSuccessful=await _authService.ResetDevice(resetId);
        if(isSuccessful)
        {
            await DisplayAlert("Alert", "Reset Successful", "OK");
        }
        else {
            await DisplayAlert("Alert", "Problem while resetting", "OK");
        }

    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}