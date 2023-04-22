using GoT_Wiki.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace GoT_Wiki.Views.DetailsViews
{
    public sealed partial class HouseDetailsPage : Page
    {
        public HouseDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var house = e.Parameter as House;
            _ = ViewModel.Load(house);
        }

        private void BackButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void CharacterTappedEventHandler(object sender, TappedRoutedEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            Frame.Navigate(typeof(CharacterDetailsPage), listViewItem.Tag);
        }

        private void HouseTappedEventHandler(object sender, TappedRoutedEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            Frame.Navigate(typeof(HouseDetailsPage), listViewItem.Tag);
        }

        private void HouseListItemClickedEventHandler(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }

        private async void PreviousCharacterBatchButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            DisableCharacterFetchButtons();
            await ViewModel.FetchPreviousBatch();
            EnableCharacterFetchButtons();
        }

        private async void NextCharacterBatchButtonClickedEventHandler(object sender, RoutedEventArgs e)
        {
            DisableCharacterFetchButtons();
            await ViewModel.FetchNextBatch();
            EnableCharacterFetchButtons();
        }

        private void DisableCharacterFetchButtons()
        {
            PreviousCharacterBatchButton.IsEnabled = false;
            NextCharacterBatchButton.IsEnabled = false;
        }

        private void EnableCharacterFetchButtons()
        {
            PreviousCharacterBatchButton.IsEnabled = true;
            NextCharacterBatchButton.IsEnabled = true;
        }

        private void CharacterListItemClickedEventHandler(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(CharacterDetailsPage), e.ClickedItem);
        }
    }
}
