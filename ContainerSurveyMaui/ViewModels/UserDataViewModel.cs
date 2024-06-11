using ContainerSurveyMaui.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
//using Windows.System;

namespace ContainerSurveyMaui.ViewModels;

public class UserDataViewModel : BaseViewModel
{

	public ObservableCollection<User> userData { get; set; }    
    private readonly AuthService _authService;

    public UserDataViewModel()
	{

		_authService = new AuthService();
        userData = new ObservableCollection<User>();
        LoadDataAsync();

    }

    private async void LoadDataAsync()
    {
        try
        {
            var data = await _authService.GetUserInfo();
            var users = JsonSerializer.Deserialize<List<User>>(data);

            foreach (var user in users)
            {

                var temp = new User { email = user.email, password = user.password, phone_number = user.phone_number,role=user.role};
                userData.Add(temp);
            }
        }
        catch (Exception)
        {

            throw;
        }


    }
}
public class User
{
    
    [DisplayName("Phone Number")]
    public string? phone_number { get; set; }
    [DisplayName("Email")]
    public string? email { get; set; }
    [DisplayName("Password")]
    public string? password { get; set; }
    public string? role { get; set; }

}