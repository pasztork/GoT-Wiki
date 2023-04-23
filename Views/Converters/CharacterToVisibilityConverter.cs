using GoT_Wiki.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GoT_Wiki.Views.Converters
{
    /// <summary>
    /// Used for converting a character model element to visibility.
    /// </summary>
    public class CharacterToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts character to visibility.
        /// </summary>
        /// <param name="value">
        /// Holds the character.
        /// </param>
        /// <returns>
        /// Visibility.Visible if the character has a name.
        /// Visibility.Collapsed if the character doesn't have a name.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is Character character && !string.IsNullOrEmpty(character.Name))
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
