namespace ContainerSurveyMaui.Pages;

public partial class MasterPage : ContentPage
{
    public MasterPage()
    {
        InitializeComponent();
    }





    protected override async void OnAppearing()
    {

        base.OnAppearing();
        bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (answer)
        {
            Logout();
        }
        else
        {
            //var Role = await SecureStorage.GetAsync("Role");
            //if(Role=="User")
            //    await Shell.Current.GoToAsync("//ViewDataPage"); 
            //else
            //    await Shell.Current.GoToAsync("//SurveyPage"); 
            Application.Current.MainPage = new NavigationPage(new AppShell());

        }
    }

    private void Logout()
    {
        SecureStorage.Default.RemoveAll();
        Application.Current.MainPage = new NavigationPage(new LoginPage());

    }


}