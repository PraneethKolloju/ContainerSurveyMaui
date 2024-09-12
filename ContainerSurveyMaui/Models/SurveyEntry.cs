using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContainerSurveyMaui.Models
{
    public class SurveyEntry : INotifyPropertyChanged
    {
        public int? id { get; set; }
        public string yard { get; set; }
        public string port { get; set; }
        public string container_No { get; set; }

        public string? location { get; set; }
        public string? shipping_line { get; set; }
        public string container_Selection { get; set; }
        public string? imei_no { get; set; }

        public string remarks { get; set; }
        private byte[] _attachment_1;
        private byte[] _attachment_2;
        private byte[] _attachment_3;
        private byte[] _attachment_4;

        public string createdOn { get; set; } 

        public byte[] attachment_1
        {
            get => _attachment_1;
            set
            {
                _attachment_1 = value;
                OnPropertyChanged();
            }
        }

        public byte[] attachment_2
        {
            get => _attachment_2;
            set
            {
                _attachment_2 = value;
                OnPropertyChanged();
            }
        }

        public byte[] attachment_3
        {
            get => _attachment_3;
            set
            {
                _attachment_3 = value;
                OnPropertyChanged();
            }
        }

        public byte[] attachment_4
        {
            get => _attachment_4;
            set
            {
                _attachment_4 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



