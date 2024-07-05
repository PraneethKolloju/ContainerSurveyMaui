using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.IO;
using System.Reflection;

namespace ContainerSurveyMaui.Pages
{
    public partial class MapsPage : ContentPage
    {
        public MapsPage(string coord)
        {
            InitializeComponent();
            LoadLocalHtmlFile(coord);
        }

        private async void LoadLocalHtmlFile(string coord)
        {

            string[] coordinates = coord.Split(',');
            string lat = coordinates[0];
            string lon = coordinates[1];

            LocationWebview.Navigated += (s, e) =>
            {
                LocationWebview.Eval($"updateMap({lat}, {lon});");
            };
        }
    }
}
