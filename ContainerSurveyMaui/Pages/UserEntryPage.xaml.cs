using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using Microsoft.Maui.Controls.Compatibility;


namespace ContainerSurveyMaui.Pages;

public partial class UserEntryPage : ContentPage
{
    private readonly IGetPostService _getPostService;
    public List<string> Ports { get; set; }
    public List<string> Yards { get; set; }
    public List<string> ShippingLines { get; set; }
    public List<string> Containers { get; set; }

    public FileResult? CapturedImage1 { get; set; }
    public FileResult? CapturedImage2 { get; set; }
    public FileResult? CapturedImage3 { get; set; }
    public FileResult? CapturedImage4 { get; set; }


    public Byte[]? imageByte1 { get; set; }
    public Byte[]? imageByte2 { get; set; }
    public Byte[]? imageByte3 { get; set; }
    public Byte[]? imageByte4 { get; set; }




    public UserEntryPage()
    {
        InitializeComponent();

        _getPostService = new GetPostSevice();

        Ports = new List<string>
            {
                "Port 1",
                "Port 2",
                "Port 3",
                "Port 4"
            };
        Yards = new List<string>
            {
                "Yard 1",
                "Yard 2",
                "Yard 3",
                "Yard 4"
            };
        ShippingLines = new List<string>
            {
                "Shipping Line 1",
                "Shipping Line 2",
                "Shipping Line 3",
                "Shipping Line 4"
            };
        Containers = new List<string>
            {
                "Containers 1",
                "Containers 2",
                "Containers 3",
                "Containers 4"
            };

        BindingContext = this;
    }
    private async void OnUploadImageClicked1(object sender, EventArgs e)
    {
        if (true)
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (cameraStatus == PermissionStatus.Granted)
            {
                try
                {
                    CapturedImage1 = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Photo"
                    });

                    if (CapturedImage1 != null)
                    {

                        var stream = await CapturedImage1.OpenReadAsync(); 
                        using (var ms = new MemoryStream())
                        {
                            SelectedImage1.IsVisible = true;
                            
                            await stream.CopyToAsync(ms);
                            imageByte1 = ms.ToArray();
                            SelectedImage1.Source = ImageSource.FromStream(() => new MemoryStream(imageByte1));

                        }

                    }
                    
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Error", "This device does not support capturing photos.", "OK");
                }
                catch (PermissionException)
                {
                    await DisplayAlert("Error", "Permission to access camera was denied.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to access camera or storage.", "OK");
            }
        }
        else
        {
            if (CapturedImage1 != null)
            {
                var stream = await CapturedImage1.OpenReadAsync();
                SelectedImage1.Source = ImageSource.FromStream(() => stream);
                SelectedImage1.IsVisible = true;
            }
        }
    }

    private async void OnUploadImageClicked2(object sender, EventArgs e)
    {
        if (true)
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (cameraStatus == PermissionStatus.Granted)
            {
                try
                {
                    CapturedImage2 = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Photo"
                    });

                    if (CapturedImage2 != null)
                    {
                        var stream = await CapturedImage2.OpenReadAsync();
                        using (var ms = new MemoryStream())
                        {
                            SelectedImage2.IsVisible = true;

                            await stream.CopyToAsync(ms);
                            imageByte2 = ms.ToArray();
                            SelectedImage2.Source = ImageSource.FromStream(() => new MemoryStream(imageByte2));
                        }
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Error", "This device does not support capturing photos.", "OK");
                }
                catch (PermissionException)
                {
                    await DisplayAlert("Error", "Permission to access camera was denied.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to access camera or storage.", "OK");
            }
        }
        else
        {
            if (CapturedImage2 != null)
            {
                var stream = await CapturedImage2.OpenReadAsync();
                SelectedImage2.Source = ImageSource.FromStream(() => stream);
                SelectedImage2.IsVisible = true;
            }
        }
    }

    private async void OnUploadImageClicked3(object sender, EventArgs e)
    {
        if (true)
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (cameraStatus == PermissionStatus.Granted)
            {
                try
                {
                    CapturedImage3 = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Photo"
                    });

                    if (CapturedImage3 != null)
                    {
                        var stream = await CapturedImage3.OpenReadAsync();
                        using (var ms = new MemoryStream())
                        {
                            SelectedImage3.IsVisible = true;

                            await stream.CopyToAsync(ms);
                            imageByte3 = ms.ToArray();
                            SelectedImage3.Source = ImageSource.FromStream(() => new MemoryStream(imageByte3));
                        }
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Error", "This device does not support capturing photos.", "OK");
                }
                catch (PermissionException)
                {
                    await DisplayAlert("Error", "Permission to access camera was denied.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to access camera or storage.", "OK");
            }
        }
        else
        {
            if (CapturedImage3 != null)
            {
                var stream = await CapturedImage3.OpenReadAsync();
                SelectedImage3.Source = ImageSource.FromStream(() => stream);
                SelectedImage3.IsVisible = true;
            }
        }
    }

    private async void OnUploadImageClicked4(object sender, EventArgs e)
    {
        if (true)
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (cameraStatus == PermissionStatus.Granted)
            {
                try
                {
                    CapturedImage4 = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Photo"
                    });

                    if (CapturedImage4 != null)
                    {
                        var stream = await CapturedImage4.OpenReadAsync();
                        using (var ms = new MemoryStream())
                        {
                            SelectedImage4.IsVisible = true;

                            await stream.CopyToAsync(ms);
                            imageByte4 = ms.ToArray();
                            SelectedImage4.Source = ImageSource.FromStream(() => new MemoryStream(imageByte4));
                        }
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Error", "This device does not support capturing photos.", "OK");
                }
                catch (PermissionException)
                {
                    await DisplayAlert("Error", "Permission to access camera was denied.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to access camera or storage.", "OK");
            }
        }
        else
        {
            if (CapturedImage4 != null)
            {
                var stream = await CapturedImage4.OpenReadAsync();
                SelectedImage4.Source = ImageSource.FromStream(() => stream);
                SelectedImage4.IsVisible = true;
            }
        }
    }

    

    private async void Button_Clicked(object sender, EventArgs e)
     {
        try
        {

            var location = await Geolocation.GetLocationAsync();

            var resultlocation= location.Latitude.ToString() + "," + location.Longitude.ToString();

            var result = _getPostService.SurveyEntry(new SurveyEntry
            {
                Port = Port.SelectedItem as String,
                Yard = Yard.SelectedItem as String,
                Shipping_line = ShipLine.SelectedItem as String,
                Container_No = ContainerNo.Text as String,
                Container_Selection = ContainerSelection.SelectedItem as String,
                Attachment_1 = imageByte1,
                Attachment_2 = imageByte2,
                Attachment_3 = imageByte3,
                Attachment_4 = imageByte4,
                Remarks = Remarks.Text,
                Location = resultlocation
            }) ;


            await DisplayAlert("Alert", "Data Saved Successfully", "OK");

            await Navigation.PushAsync(new HomePage());
            

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationPage.SetHasBackButton(this, false);
    }
}