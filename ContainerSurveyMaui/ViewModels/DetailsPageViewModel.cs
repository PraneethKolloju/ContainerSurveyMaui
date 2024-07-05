using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContainerSurveyMaui.ViewModels
{
    public class DetailsPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ImageItem> _imageItems;
        public ObservableCollection<ImageItem> ImageItems
        {
            get => _imageItems;
            set
            {
                _imageItems = value;
                OnPropertyChanged();
            }
        }

        //public DetailsPageViewModel()
        //{
        //    // Initialize with some data for example purposes
        //    ImageItems = new ObservableCollection<ImageItem>
        //    {
        //        new ImageItem { Name = "Image 1", ImageData  },
        //        new ImageItem { Name = "Image 2", ImageData = /* your byte array here */ }
        //    };
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ImageItem
    {
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
    }
}
