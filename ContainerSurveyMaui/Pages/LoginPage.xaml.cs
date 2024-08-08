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
        SecureStorage.SetAsync("isLoggedIn", "notok");
        _authService = new AuthService();

    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            var email = userNameEntry.Text;
            var password = passwordEntry.Text;

            if (email == null || password == null || email=="" || password=="")
            {
                if (email == null || email == "")
                {
                    nameValidation.Text = "Enter Valid Username";
                }
                else
                {
                    nameValidation.Text = "";
                }
                if (password == null || password == "")
                {
                    pwdValidations.Text = "Enter Valid Password";
                }
                else
                {
                    pwdValidations.Text = "";
                }
                //Application.Current.MainPage = new NavigationPage(new LoginPage());

            }
            else
            {

                overlay.IsVisible = true;
                mainLayout.IsEnabled = false;

                bool result = await _authService.LoginApi(email, password);

                if (result)
                {
                    SecureStorage.SetAsync("isLoggedIn","ok");
                    ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
                    Application.Current.MainPage = new NavigationPage(new AppShell());
                }
                else
                    await DisplayAlert("Alert", "Incorrect Credentials", "ok");
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            overlay.IsVisible = false;
            mainLayout.IsEnabled = true;
        }

    }

    private async void Bio_Clicked(object sender, EventArgs e)
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
        await Navigation.PushAsync(new ResetPwdPage());

    }
}