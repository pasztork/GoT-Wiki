using GoT_Wiki.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace GoT_Wiki.Views.DetailsViews
{
    public sealed partial class CharacterDetailsPage : Page
    {
        public CharacterDetailsPage()
        {
            InitializeComponent();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
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

        private void CharacterTapped(object sender, TappedRoutedEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            Frame.Navigate(typeof(CharacterDetailsPage), listViewItem.Tag);
        }

        private void AllegianceListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }

        private void BookListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(BookDetailsPage), e.ClickedItem);
        }
    }
}
