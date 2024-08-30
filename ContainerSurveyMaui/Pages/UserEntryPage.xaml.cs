//using Android.Gms.Common.Apis;
using CommunityToolkit.Maui.Behaviors;
using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using UraniumUI.Material.Controls;
using System.Text.RegularExpressions;




namespace ContainerSurveyMaui.Pages;

public partial class UserEntryPage : ContentPage
{
    private readonly IGetPostService _getPostService;

    private ObservableCollection<string> _ports;
    public ObservableCollection<string> Ports
    {
        get => _ports;
        set
        {
            _ports = value;
            OnPropertyChanged();
        }
    }
    private ObservableCollection<string> _yards;
    public ObservableCollection<string> Yards
    {
        get => _yards;
        set
        {
            _yards = value;
            OnPropertyChanged();
        }
    }
    private ObservableCollection<string> _shippinglines;
    public ObservableCollection<string> ShippingLines
    {
        get => _shippinglines;
        set
        {
            _shippinglines = value;
            OnPropertyChanged();
        }
    }

    public List<string> Containers { get; set; }

    public FileResult? CapturedImage1 { get; set; }
    public FileResult? CapturedImage2 { get; set; }
    public FileResult? CapturedImage3 { get; set; }
    public FileResult? CapturedImage4 { get; set; }

    private bool _isBtnEnabled = true;
    public bool IsBtnEnabled
    {
        get => _isBtnEnabled;
        set
        {
            _isBtnEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool _isLoaderBusy = false;
    public bool IsLoaderBusy
    {
        get => _isLoaderBusy;
        set
        {
            _isLoaderBusy = value;
            OnPropertyChanged();
        }
    }

    private string _submitButtonText = "Submit";
    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged();
        }
    }




    public Byte[]? imageByte1 { get; set; }
    public Byte[]? imageByte2 { get; set; }
    public Byte[]? imageByte3 { get; set; }
    public Byte[]? imageByte4 { get; set; }

    public int imagesCount = 0;

    public UserEntryPage()
    {
        InitializeComponent();

        _getPostService = new GetPostSevice();

        Ports = new ObservableCollection<string>();

        Yards = new ObservableCollection<string>();

        ShippingLines = new ObservableCollection<string>();

        Containers = new List<string>
            {
                "Selected",
                "to be repaired",
                "Rejected",

            };
        BindingContext = this;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        MasterDataLoad();
        var firsttime = await SecureStorage.GetAsync("firsttime_user");
        if (firsttime == "1")
        {
            //await Task.Delay(3000);
            await DisplayAlert("Alert", "You're a New User, Reset the password", "OK");

            await Navigation.PushAsync(new ResetPwdPage());

        }

    }

    public class ApiResponse
    {
        [JsonPropertyName("port")]
        public List<PortMaster> Port { get; set; }

        [JsonPropertyName("yard")]
        public List<YardMaster> Yard { get; set; }

        [JsonPropertyName("shippingLine")]
        public List<ShippingLineMaster> ShippingLines { get; set; }
    }

    private async void MasterDataLoad()
    {
        var MasterData = await _getPostService.GetMasterData(null);
        var Data = JsonSerializer.Deserialize<ApiResponse>(MasterData);
        Ports.Clear();

        foreach (var port in Data.Port)
        {
            Ports.Add(port.Port.Trim());
        }

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
                            if (imageByte1 != null)
                                imagesCount += 1;
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
                            if (imageByte2 != null)
                                imagesCount += 1;
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
                            if (imageByte3 != null)
                                imagesCount += 1;
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
                            if (imageByte4 != null)
                                imagesCount += 1;
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
        IsLoaderBusy = true;
        IsBtnEnabled = false;
        SubmitButtonText = "";
        try
        {
            var regex = "^[A-Za-z]{4}\\d{7}$";

            var regexValid = Regex.IsMatch(ContainerNo.Text, regex);

            if (!regexValid)
            {
                await DisplayAlert("Error", "Enter Valid Container No", "Ok");
                return;
            }

            if (Port.SelectedIndex == -1 || Yard.SelectedIndex == -1 || ShipLine.SelectedIndex == -1 || ContainerSelection.SelectedIndex == -1 || ContainerNoValid.IsNotValid || RemarksValid.IsNotValid)
            {
                await DisplayAlert("Error", "Enter All Required Fields", "Ok");
                return;
            }
            if (ContainerSelection.SelectedIndex == 0)
            {
                if (imagesCount < 3)
                {
                    await DisplayAlert("Error", "Atleast 3 attachments are required", "Ok");
                    return;
                }
            }
            else if (ContainerSelection.SelectedIndex == 1 || ContainerSelection.SelectedIndex == 2)
            {
                if (imagesCount < 1)
                {
                    await DisplayAlert("Error", "Atleast 1 attachment is required", "Ok");
                    return;
                }
            }

            var location = await Geolocation.GetLocationAsync();

            var resultlocation = location.Latitude.ToString() + "," + location.Longitude.ToString();

            var result = _getPostService.SurveyEntry(new SurveyEntry
            {
                port = Port.SelectedItem as String,
                yard = Yard.SelectedItem as String,
                shipping_line = ShipLine.SelectedItem as String,
                container_No = ContainerNo.Text as String,
                container_Selection = ContainerSelection.SelectedItem as String,
                attachment_1 = imageByte1,
                attachment_2 = imageByte2,
                attachment_3 = imageByte3,
                attachment_4 = imageByte4,
                remarks = Remarks.Text,
                location = resultlocation
            });

            //await Task.Delay(2000);
            await DisplayAlert("Alert", "Data Saved Successfully", "OK");

            await Navigation.PushAsync(new UserEntryPage());

        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert",ex.Message, "OK");
            return;
        }
        finally
        {
            IsLoaderBusy = false;
            IsBtnEnabled = true;
            SubmitButtonText = "Submit";
        }
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void Port_SelectedValueChanged(object sender, object e)
    {

        var selectedValue = "";
        var selectedIndex = -2;
        if (sender is PickerField pickerField)
        {
            if (pickerField.SelectedItem != null)
                selectedValue = pickerField.SelectedItem.ToString() ?? "";
            selectedIndex = pickerField.SelectedIndex;
        }
        try
        {
            if (selectedIndex < 0)
            {
                Yards.Clear();
                ShippingLines.Clear();
                DisplayAlert("Warning", "Select the Port", "ok");
                return;
            }
            var MasterData = await _getPostService.GetMasterData(selectedValue);

            var Data = JsonSerializer.Deserialize<ApiResponse>(MasterData);

            if (Data == null)
            {
                Debug.WriteLine("Deserialized Data is null");
                return;
            }

            Yards.Clear();
            ShippingLines.Clear();

            if (Data.Yard != null)
            {
                foreach (var yard in Data.Yard)
                {
                    if (yard != null && yard.Yard != null)
                    {
                        Yards.Add(yard.Yard.Trim());
                    }
                    else
                    {
                        Debug.WriteLine($"Null value found: Yard = {yard}, Yard.Yard = {yard?.Yard}");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Data.Yard is null");
            }

            if (Data.ShippingLines != null)
            {
                foreach (var shipping in Data.ShippingLines)
                {
                    if (shipping != null && shipping.ShippingLine != null)
                    {
                        ShippingLines.Add(shipping.ShippingLine.Trim());
                    }
                    else
                    {
                        Debug.WriteLine($"Null value found: Shipping = {shipping}, Shipping.ShippingLine = {shipping?.ShippingLine}");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Data.ShippingLines is null");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception occurred: {ex.Message}");
        }

    }
}