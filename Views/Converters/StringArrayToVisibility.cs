using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GoT_Wiki.Views.Converters
{
    public class StringArrayToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string[] strArray && strArray.Length > 0 && !string.IsNullOrEmpty(strArray[0]))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
