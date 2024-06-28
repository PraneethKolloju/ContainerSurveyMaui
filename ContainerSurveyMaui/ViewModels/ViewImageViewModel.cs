    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    namespace ContainerSurveyMaui.ViewModels
    {
        public class ViewImageViewModel : INotifyPropertyChanged
        {
            private byte[] vbImage;

            public byte[] VbImage
            {
                get { return vbImage; }
                set
                {
                    if (vbImage != value)
                    {
                        vbImage = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
