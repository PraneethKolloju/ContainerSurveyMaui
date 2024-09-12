using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.ViewModels;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace ContainerSurveyMaui.Pages
{
    public partial class DetailsPage : ContentPage
    {
        //public List<SurveyDetails> ImageDataDemoList { get; set; } = new List<SurveyDetails>();

        private ViewImageViewModel viewModel;
        public DetailsPage(int i)
        {
            Preferences.Set("Id", i);
            InitializeComponent();
            viewModel = new ViewImageViewModel();
            BindingContext = viewModel;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new SurveyPage());
        }


        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyPage());
        }
        double currentScale = 1;
        double startScale = 1;


    }
}
