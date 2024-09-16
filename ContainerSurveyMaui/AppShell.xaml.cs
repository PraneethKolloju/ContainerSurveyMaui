using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Services;
using System.Diagnostics;

namespace ContainerSurveyMaui
{
    public partial class AppShell : Shell
    {
        private AuthService _authService;
        public string Role { get; set; }

        public AppShell()
        {
            InitializeComponent();
            _authService = new AuthService();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ConfigureTabs();
             LoadPopup();
        }

        private async Task LoadPopup()
        {
            var firsttime = await SecureStorage.GetAsync("firsttime_user");
            //if (firsttime == "1")
            //{
            //    await Task.Delay(3000);
            //    await DisplayAlert("Alert", "You're a New User, Reset the password", "OK");
                
            //    await Navigation.PushAsync(new ResetPwdPage());
                
            //}
        }



        public async Task ConfigureTabs()
        {

            try
            {
                var Role =await  SecureStorage.GetAsync("Role");
                if (Role == "User")
                {
                    CurrentItem = ViewDataPage;
                    ViewDataPage.IsVisible = true;
                    AdminPage.IsVisible = false;
                    MasterPage.IsVisible = true;
                }
                else if (Role == "Admin")
                {
                    CurrentItem = SurveyPage;
                    SurveyPage.IsVisible = true;
                    AdminPage.IsVisible = true;
                    MasterPage.IsVisible = true;
                }
                else
                {
                    MasterPage.IsVisible = false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }




        

        private void Logout()
        {
            SecureStorage.Default.RemoveAll();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
            AdminPage.IsVisible = false;
            ViewDataPage.IsVisible = false;
            SurveyPage.IsVisible = false;
        }


    }
}
