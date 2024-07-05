using ContainerSurveyMaui.Models;
using ContainerSurveyMaui.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ContainerSurveyMaui.ViewModels;

public class SurveyDetailsViewModel : BaseViewModel
{
    public ObservableCollection<SurveyDetails> SurveyData { get; set; }
    private readonly GetPostSevice _getpostservice;
    public SurveyDetailsViewModel(int id)
	{
		_getpostservice = new GetPostSevice();

        SurveyData= new ObservableCollection<SurveyDetails>();
	}

    


}