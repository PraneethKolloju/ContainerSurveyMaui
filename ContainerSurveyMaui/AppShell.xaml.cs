using ContainerSurveyMaui.Pages;
using ContainerSurveyMaui.Services;
using Syncfusion.Maui.Popup;

namespace ContainerSurveyMaui
{
    public partial class AppShell : Shell
    {
        private AuthService _authService;
        public string Role { get; set; }

        public AppShell()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhPYVF1WmFZfVpgcV9CYFZSQWYuP1ZhSXxXdkBhXX9bdHRWQGhbV0M=");
            InitializeComponent();
            _authService = new AuthService();
            ConfigureTabs().ConfigureAwait(false);
            LoadPopup().ConfigureAwait(false);
        }

        private async Task LoadPopup()
        {
            var firsttime = await SecureStorage.GetAsync("firsttime_user");
            if (firsttime == "1")
            {
                await DisplayAlert("Alert", "You're a New User, Reset the password", "OK");
                await Navigation.PushAsync(new ResetPwdPage());
            }
        }

        public async Task ConfigureTabs()
        {
            try
            {
                var Role = await SecureStorage.GetAsync("Role");
                if (Role == "User")
                    AdminPage.IsVisible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
