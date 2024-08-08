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

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public UserDataViewModel()
	{

		_authService = new AuthService();
        IsLoading = true;
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
        finally
        {
            IsLoading = false;

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
    [DisplayName("Role")]
    public string? role { get; set; }

}