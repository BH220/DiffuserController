using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DiffuserControllerNew.Converter
{
    public class BoolToImageConverter : IValueConverter
    {
        public string CheckedImage { get; set; }
        public string UncheckedImage { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = value is bool b && b;
            string path = isChecked ? CheckedImage : UncheckedImage;
            return new BitmapImage(new Uri($"pack://application:,,,{path}", UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
