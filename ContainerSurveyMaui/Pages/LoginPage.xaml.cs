using ContainerSurveyMaui.Services;
using Plugin.Maui.Biometric;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ContainerSurveyMaui.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;
    public LoginPage()
	{
		InitializeComponent();
        _authService = new AuthService();

    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            var email = userNameEntry.Text;
            var password = passwordEntry.Text;

            bool result = await _authService.LoginApi(email, password);

            if (result)
            {
                ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
                await Navigation.PushAsync(new AppShell());
            }
            else
                await DisplayAlert("Alert", "Incorrect Credentials", "ok");
        }
        catch (Exception)
        {

            throw;
        }
      
    }

    private async void Bio_Clicked(object sender,EventArgs e)
    {
        try
        {
            var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new
              AuthenticationRequest()
            {
                Title = "Please Authenticate",
                NegativeText = "Cancel authentication"
            }, CancellationToken.None);

            if (result.Status == BiometricResponseStatus.Success)
                await Navigation.PushAsync(new AppShell());

            else
                await DisplayAlert("Alert", "Incorrect Credentials", "OK");

        }
        catch (Exception)
        {

            throw;
        }
    }
}