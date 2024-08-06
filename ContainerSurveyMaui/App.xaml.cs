﻿using ContainerSurveyMaui.Pages;

namespace ContainerSurveyMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new NavigationPage(new LoginPage());
        }
        

    }
}
