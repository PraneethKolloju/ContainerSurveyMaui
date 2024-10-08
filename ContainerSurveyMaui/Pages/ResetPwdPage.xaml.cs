using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Graphics;
using ContainerSurveyMaui.Services;
using System.Text.RegularExpressions;
using DeviceDetails;
using CommunityToolkit.Maui.Converters;
namespace ContainerSurveyMaui.Pages;

public partial class ResetPwdPage : ContentPage
{
    private readonly IAuthService _authService;

    public GetDeviceInfo _getDeviceInfo;
    public ResetPwdPage()
    {
        InitializeComponent();
        _authService = new AuthService();
        _getDeviceInfo = new GetDeviceInfo();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();


    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs txt)
    {
        var userName = username.Text;
        string rePassword = txt.NewTextValue;

        var passWord = newpassword.Text;
        if (passWord == rePassword)
        {
            pwdMatch.Text = "passwords match";
            pwdMatch.TextColor = new Color(0, 173, 0);
            resetbtn.IsEnabled = true;
        }
        else
        {
            pwdMatch.Text = "passwords doesnot match";
            pwdMatch.TextColor = new Color(173, 0, 0);
            resetbtn.IsEnabled = false;
        }
    }

    private async void button_Clicked(object sender, EventArgs e)
    {
        try
                {
            var userName = username.Text;
            var oldPassword = password.Text;
            var newPassword = newpassword.Text;
            var rePassword = repassword.Text;
            var res = false;

            if (!String.IsNullOrEmpty(oldPassword))
                res = await _authService.LoginApi(userName, oldPassword);

            if (userName == null || oldPassword == null || newPassword == null || rePassword == null || userName == "" || oldPassword == "" || newPassword == "" || rePassword == "")
            {
                await DisplayAlert("Error", "All Fields are Required", "OK");
                return;
            }
            else if (!res)
            {
                await DisplayAlert("Error", "Incorrect Username/Password", "OK");
                return;
            }
            else
            {
                if (newPassword == rePassword)
                {
                    resetbtn.IsEnabled = true;
                }
                else
                {
                    resetbtn.IsEnabled = false;
                }
                var imei_no = _getDeviceInfo.GetDeviceID();

                bool result = await _authService.Resetpassword(userName, oldPassword, newPassword, imei_no);
                if (result)
                {
                    await SecureStorage.SetAsync("firsttime_user", "0");
                    await DisplayAlert("Success", "Password Reset Successfully", "OK");
                    var LoggedIn = await SecureStorage.GetAsync("isLoggedIn");
                    if (LoggedIn == "ok")
                    {
                        var Role = await SecureStorage.GetAsync("Role");
                        if (Role == "Admin")
                            await Navigation.PushAsync(new SurveyPage());
                        else
                            await Navigation.PushAsync(new UserEntryPage());

                    }
                    else
                        await Navigation.PushAsync(new LoginPage());

                }
                else
                    await DisplayAlert("Alert", "Problem while resetting password", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
            return;
        }
    }
    private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        string newPassword = e.NewTextValue;
        Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$");
        Match match = regex.Match(newPassword);
        if (match.Success)
        {
            newpasslabel.Text = "strong password";
            newpasslabel.TextColor = new Color(0, 173, 0);
            resetbtn.IsEnabled = true;
        }
        else
        {
            newpasslabel.Text = "weak password";
            newpasslabel.TextColor = new Color(173, 0, 0);
            resetbtn.IsEnabled = false;
        }
    }

    private async void Entry_TextChanged_2(object sender, TextChangedEventArgs e)
    {
        var userName = username.Text;
        string oldPassword = e.NewTextValue;
        var res = await _authService.LoginApi(userName, oldPassword);
        if (res != true)
        {
            validationMsg.Text = "Incorrect Username/Password";
            validationMsg.TextColor = new Color(173, 0, 0);
            resetbtn.IsEnabled = false;
        }
        else
        {
            validationMsg.Text = "";
            validationMsg.TextColor = new Color(0, 0, 0);
            resetbtn.IsEnabled = true;
        }


    }











}


