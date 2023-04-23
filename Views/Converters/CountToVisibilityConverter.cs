using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GoT_Wiki.Views.Converters
{
    /// <summary>
    /// Used for converting an int to visibility.
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts int (more precisely the size of an iterable) to visibility.
        /// </summary>
        /// <param name="value">
        /// Holds the number.
        /// </param>
        /// <returns>
        /// Visibility.Visible if number &gt; 0; 
        /// Visibility.Collapsed otherwise.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int count && count > 0)
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
