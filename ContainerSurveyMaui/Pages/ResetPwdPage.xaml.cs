using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Graphics;
using ContainerSurveyMaui.Services;
using System.Text.RegularExpressions;

namespace ContainerSurveyMaui.Pages;

public partial class ResetPwdPage : ContentPage
{
    private readonly IAuthService _authService;
    public ResetPwdPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationPage.SetHasBackButton(this, false);
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs txt)
    {
        var userName = username.Text;
        string rePassword = txt.NewTextValue;

        var passWord = newpassword.Text;
        if (passWord != rePassword)
        {
            pwdMatch.Text = "passwords doesnot match";
            pwdMatch.TextColor = new Color(173,0 , 0);
            resetbtn.IsEnabled = false;
        }
        else
        {
            pwdMatch.Text = "passwords match";
            pwdMatch.TextColor = new Color(0, 173, 0);
            resetbtn.IsEnabled = true;

        }
    }

    private async void button_Clicked(object sender, EventArgs e)
    {
        var userName = username.Text;
        var oldPassword = password.Text;
        var newPassword = newpassword.Text;

        bool result = await _authService.Resetpassword(userName, oldPassword, newPassword);
        if (result)
        {
            await DisplayAlert("Ok", "Password Reset Successfully", "ok");
            await Navigation.PushAsync(new UserEntryPage());
        }
        else
            await DisplayAlert("Alert", "Problem while resetting password", "ok");
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


