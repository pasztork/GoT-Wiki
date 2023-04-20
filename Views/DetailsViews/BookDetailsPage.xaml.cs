using GoT_Wiki.Models;
using GoT_Wiki.Views.DetailsViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoT_Wiki.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookDetailsPage : Page
    {
        public BookDetailsPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var book = e.Parameter as Book;
            _ = ViewModel.Load(book);
        }

        private async void NextCharacterBatchButton_Click(object sender, RoutedEventArgs e)
        {
            DisableCharacterFetchButtons();
            await ViewModel.FetchNextBatch();
            EnableCharacterFetchButtons();
        }

        private async void PreviousCharacterBatchButton_Click(object sender, RoutedEventArgs e)
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

        private async void NextPovCharactersButton_Click(object sender, RoutedEventArgs e)
        {
            DisablePovCharacterFetchButtons();
            await ViewModel.FetchNextPovCharacterBatch();
            EnablePovCharacterFetchButtons();
        }

        private async void PreviousPovCharactersButton_Click(object sender, RoutedEventArgs e)
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

        private void CharacterList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(CharacterDetailsPage), e.ClickedItem);
        }
    }
}
