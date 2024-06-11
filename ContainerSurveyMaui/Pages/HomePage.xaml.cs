using System.Text;
using System.Text.Json;
using ContainerSurveyMaui.Constants;

namespace ContainerSurveyMaui.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
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
        string role = Role.Text;

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
            var resp = await httpClient.PostAsync("api/Api/Register", requestData);
            string w = resp.StatusCode.ToString();
            //DisplayAlert("Alert",w, "OK");
            if (w == "OK")
            {
                await Navigation.PushAsync(new ViewDataPage());
                await DisplayAlert("Ok", "User Created Successfully", "OK");
            }
            else
                await DisplayAlert("Alert", "Problem while creating User", "OK");

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

    private void passwordEntry_Loaded(object sender, EventArgs e)
    {

    }
}