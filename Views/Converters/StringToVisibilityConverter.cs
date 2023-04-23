using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GoT_Wiki.Views.Converters
{
    /// <summary>
    /// Used for converting a string to visibility.
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts string to visibility.
        /// </summary>
        /// <param name="value">
        /// Holds the string.
        /// </param>
        /// <returns>
        /// Visibility.Visible if the is not empty or null.
        /// Visibility.Collapsed otherwise.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Never used. Throws exception.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
