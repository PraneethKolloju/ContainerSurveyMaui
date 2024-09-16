using ContainerSurveyMaui.Services;
using DeviceDetails;
using Plugin.Maui.Biometric;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ContainerSurveyMaui.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;
    public GetDeviceInfo _getDeviceInfo;

    public LoginPage()
    {
        InitializeComponent();
        SecureStorage.SetAsync("isLoggedIn", "notok");
        _authService = new AuthService();
        _getDeviceInfo = new GetDeviceInfo();
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            var email = userNameEntry.Text;
            var password = passwordEntry.Text;

            if (email == null || password == null || email == "" || password == "")
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

            }
            else
            {

                overlay.IsVisible = true;
                mainLayout.IsEnabled = false;


                bool result = await _authService.LoginApi(email, password);

                if (result)
                {
                    await SecureStorage.SetAsync("LoggedInName", email.ToString());
                    var deviceId = _getDeviceInfo.GetDeviceID();
                    var existingDeviceId = await _authService.GetDeviceId(email, password);
                    var temp = existingDeviceId.Count();
                    if (temp != 0)
                    {
                        if (existingDeviceId != deviceId)
                        {
                            await DisplayAlert("Alert", "Please Login using the Registered Device", "OK");
                            return;
                        }
                    }
                    SecureStorage.SetAsync("isLoggedIn", "ok");
                    ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
                    Application.Current.MainPage = new NavigationPage(new AppShell());
                }
                else
                {
                    var msg = await SecureStorage.GetAsync("login_error");
                    if (msg != "Unauthorized")
                    {
                        await DisplayAlert("Alert", msg, "OK");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Incorrect Credentials", "OK");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
            return;
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