using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using ContainerSurveyMaui.Constants;
using ContainerSurveyMaui.Services;

namespace ContainerSurveyMaui.Pages;

public partial class HomePage : ContentPage
{

    private readonly AuthService _authService;
    public HomePage()
    {
        InitializeComponent();
        _authService = new AuthService();
        List<string> roles = new List<string>
            {
                "Admin",
                "User",
            };
        Role.ItemsSource = roles;
    }


    private void Generate_Pwd(object sender, EventArgs e)
    {
        var password = GenerateRandomPassword(10);
        passwordEntry.Text = password;
    }
    private string GenerateRandomPassword(int length)
    {
        try
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        catch (Exception)
        {

            throw;
        }
    }



    private async void Button_Clicked(object sender, EventArgs e)
    {
        var httpClientHandler = new HttpClientHandler();
        using var httpClient = new HttpClient(httpClientHandler);

        httpClient.BaseAddress = new Uri(Constants.Constants.BaseUrl);

        string email = userNameEntry.Text;
        string phone_number = PhoneEntry.Text;
        string password = passwordEntry.Text;
        string role = Role.SelectedItem as String;

        if (email == null || phone_number == null || password == null || role == null || email == "" || phone_number == "" || password == "" || role == "")
        {
            await DisplayAlert("Error", "All Fields are Required", "Ok");
            return;
        }
        else
        {

            string jsonData = JsonSerializer.Serialize(new
            {
                email,
                password,
                phone_number,
                role

            });
            var requestData = new StringContent(jsonData, Encoding.UTF8, "application/json");


            try
            {
                var token = await _authService.GetToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var resp = await httpClient.PostAsync("api/Api/Register", requestData);
                string w = resp.StatusCode.ToString();
                //DisplayAlert("Alert",w, "OK");
                if (w == "OK")
                {
                    await Navigation.PushAsync(new ViewDataPage());
                    await DisplayAlert("Success", "User Created Successfully", "OK");
                }
                else
                {
                    string errorMessage = await resp.Content.ReadAsStringAsync();
                    await DisplayAlert("Alert", errorMessage, "OK");
                }

            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Danger", ex.Message, "Cancel");
            }
        }
    }



    private void passwordEntry_Loaded(object sender, EventArgs e)
    {

    }

    private void PhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        Regex RegexExp = new Regex(@"^(\+91[\-\s]?)?[0]?(91)?[6789]\d{9}$");
        var phnEntry = e.NewTextValue;
        Match regexMatch = RegexExp.Match(phnEntry);
        if (!regexMatch.Success)
        {
            validationStatement.Text = "Invalid Phone Number";
            validationStatement.TextColor = new Color(173, 0, 0);
            CreateBtn.IsEnabled = false;
        }
        else
        {
            validationStatement.Text = "";
            CreateBtn.IsEnabled = true;

        }




    }
}