using ContainerSurveyMaui.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using static ContainerSurveyMaui.Pages.UserEntryPage;

namespace ContainerSurveyMaui.Pages;

public partial class PortDetailsPage : ContentPage
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
    public PortDetailsPage()
    {
        InitializeComponent();
        _getPostService = new GetPostSevice();

        Ports = new ObservableCollection<string>();

        Yards = new ObservableCollection<string>();

        ShippingLines = new ObservableCollection<string>();
        MasterDataLoad();

        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        MasterDataLoad();

    }
    private async void MasterDataLoad()
    {
        try
        {
            var MasterData = await _getPostService.GetMasterData(null);
            var Data = JsonSerializer.Deserialize<ApiResponse>(MasterData);
            Ports.Clear();

            foreach (var port in Data.Port)
            {
                Ports.Add(port.Port.Trim());
            }
            Ports.Add("Others");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
        }
    }

    private async void Port_SelectedValueChanged(object sender, object e)
    {
        var ddPortValue = Port.SelectedItem as string;
        if(ddPortValue =="Others") {
            Porttxt.IsVisible = true;
            Yardtxt.IsVisible=true;
            ShippingLinetxt.IsVisible=true; 
            Yard.IsVisible=false;
            ShippingLine.IsVisible=false;   
        }
        else
        {
            Porttxt.IsVisible = false;
            Yardtxt.IsVisible = false;
            ShippingLinetxt.IsVisible = false;
            Yard.IsVisible = true;
            ShippingLine.IsVisible = true;
        }

        try
        {
            var MasterData = "";
            if(ddPortValue != "Others")
                MasterData = await _getPostService.GetMasterData(ddPortValue);

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
                Yards.Add("Others");
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
                ShippingLines.Add("Others");
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

    private async void Yard_SelectedValueChanged(object sender, object e)
    {
        try
        {
            var ddYard = Yard.SelectedItem as String;
            if (ddYard == "Others")
            {
                Yardtxt.IsVisible = true;
            }
            else
            {
                Yardtxt.IsVisible = false;
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Alert", ex.Message, "OK");
            return;
        }
    }

    private async void ShippingLine_SelectedValueChanged(object sender, object e)
    {
        try
        {
            var ddShippingLine = ShippingLine.SelectedItem as String;
            if (ddShippingLine == "Others")
            {
                ShippingLinetxt.IsVisible = true;
            }
            else
            {
                ShippingLinetxt.IsVisible = false;
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Alert", ex.Message, "OK");
            return;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var PortValue = "";
            var YardValue = "";
            var ShippingLinesValue = "";

            if (Port.SelectedItem as String == "Others" || Port.SelectedItem as String == null)
                PortValue = Porttxt.Text;
            else
                PortValue = Port.SelectedItem as String;

            if (Yard.SelectedItem as String == "Others" || Yard.SelectedItem as String == null)
                YardValue = Yardtxt.Text;
            else
                YardValue = Yard.SelectedItem as String;

            if (ShippingLine.SelectedItem as String == "Others" || ShippingLine.SelectedItem as String == null)
                ShippingLinesValue = ShippingLinetxt.Text;
            else
                ShippingLinesValue = ShippingLine.SelectedItem as String;

            if (String.IsNullOrEmpty(PortValue) || String.IsNullOrEmpty(YardValue) || String.IsNullOrEmpty(ShippingLinesValue))
            {
                await DisplayAlert("Alert", "Enter Required Fields", "OK");
                return;
            }



            var loggedInName = await SecureStorage.GetAsync("LoggedInName");

            NewPortEntry inputPortData = new NewPortEntry
            {
                Port = PortValue,
                Yard = YardValue,
                ShippingLine = ShippingLinesValue,
                loggedInName = loggedInName
            };

            var response = await _getPostService.NewPortEntry(inputPortData);
            if (response == "Success")
            {
                MasterDataLoad();
                await DisplayAlert("Alert", "Inserted Successfully", "OK");
                return;
            }

            else
            {
                await DisplayAlert("Alert", response, "OK");
                return;
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }
}