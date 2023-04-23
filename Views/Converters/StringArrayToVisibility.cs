using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GoT_Wiki.Views.Converters
{
    /// <summary>
    /// Used for converting an array of strings to visibility.
    /// </summary>
    public class StringArrayToVisibility : IValueConverter
    {
        /// <summary>
        /// Converts strings to visibility.
        /// </summary>
        /// <param name="value">
        /// Holds the strings.
        /// </param>
        /// <returns>
        /// Visibility.Visible if the there is at least one string in the array and it is not empty or null.
        /// Visibility.Collapsed otherwise.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string[] strArray && strArray.Length > 0 && !string.IsNullOrEmpty(strArray[0]))
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
