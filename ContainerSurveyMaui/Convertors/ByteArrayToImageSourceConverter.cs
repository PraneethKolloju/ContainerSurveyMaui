using System;
using System.Globalization;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Drawing;

namespace ContainerSurveyMaui.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                var stream = new MemoryStream(byteArray);
                return ImageSource.FromStream(() => stream);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
