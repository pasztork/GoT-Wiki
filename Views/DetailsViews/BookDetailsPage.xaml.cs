using GoT_Wiki.Models;
using GoT_Wiki.Views.DetailsViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GoT_Wiki.Views
{
    /// <summary>
    /// Used by View for showing BookDetailsPage.
    /// Contains all methods triggered by UI components.
    /// </summary>
    public sealed partial class BookDetailsPage : Page
    {
        /// <summary>
        /// Public ctor. Initializes component.
        /// </summary>
        public BookDetailsPage()
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
            var book = e.Parameter as Book;
            _ = ViewModel.Load(book);
        }

        private async void NextCharacterBatchButtonClicked(object sender, RoutedEventArgs e)
        {
            DisableCharacterFetchButtons();
            await ViewModel.FetchNextBatch();
            EnableCharacterFetchButtons();
        }

        private async void PreviousCharacterBatchButtonClicked(object sender, RoutedEventArgs e)
        {
            DisableCharacterFetchButtons();
            await ViewModel.FetchPreviousBatch();
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

        private async void NextPovCharactersButtonClicked(object sender, RoutedEventArgs e)
        {
            DisablePovCharacterFetchButtons();
            await ViewModel.FetchNextPovCharacterBatch();
            EnablePovCharacterFetchButtons();
        }

        private async void PreviousPovCharactersButtonClicked(object sender, RoutedEventArgs e)
        {
            DisablePovCharacterFetchButtons();
            await ViewModel.FetchPreviousPovCharacterBatch();
            EnablePovCharacterFetchButtons();
        }

        private void DisablePovCharacterFetchButtons()
        {
            PreviousPovCharactersButton.IsEnabled = false;
            NextPovCharactersButton.IsEnabled = false;
        }

        private void EnablePovCharacterFetchButtons()
        {
            PreviousPovCharactersButton.IsEnabled = true;
            NextPovCharactersButton.IsEnabled = true;
        }

        private void CharacterListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(CharacterDetailsPage), e.ClickedItem);
        }
    }
}
