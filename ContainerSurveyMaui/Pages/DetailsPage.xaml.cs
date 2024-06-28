using ContainerSurveyMaui.ViewModels;

namespace ContainerSurveyMaui.Pages;

public partial class DetailsPage : ContentPage
{
    
    public DetailsPage(byte[] imageData)
    {
        InitializeComponent();
        DisplayImage(imageData);
    }
    private void DisplayImage(byte[] imageBytes)
    {
        try
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                DisplayAlert("Error", "Image data is invalid", "OK"); 
                return;
            }
            var imageStream = new MemoryStream(imageBytes);
            ImageMaui.Source = ImageSource.FromStream(() => imageStream);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}