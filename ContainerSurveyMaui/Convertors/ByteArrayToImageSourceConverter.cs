using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using System.IO;

namespace ContainerSurveyMaui.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                // Check if the parameter is to verify if the byte array is null or empty
                if (parameter.ToString() == "IsNullOrEmpty")
                {
                    return value == null || ((byte[])value).Length == 0;
                }
                else if (parameter.ToString() == "IsNotNullOrEmpty")
                {
                    return value != null && ((byte[])value).Length > 0;
                }
            }

            // Convert byte array to ImageSource if not null and not empty
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
