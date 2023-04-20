using GoT_Wiki.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoT_Wiki.Views.DetailsViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterDetailsPage : Page
    {
        public CharacterDetailsPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var character = e.Parameter as Character;
            _ = ViewModel.Load(character);
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Frame.Navigate(typeof(CharacterDetailsPage), button.Tag);
        }

        private void AllegiancesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }

        private void Book_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(BookDetailsPage), e.ClickedItem);
        }

        private void Button_Hovered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var button = sender as Button;
            button.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
