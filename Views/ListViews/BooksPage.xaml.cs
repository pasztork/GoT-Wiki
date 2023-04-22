using GoT_Wiki.Models;
using GoT_Wiki.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GoT_Wiki.Views
{
    public sealed partial class BooksPage : Page
    {
        private readonly ListViewModel<Book> _viewModel = null;

        public BooksPage()
        {
            InitializeComponent();
            _viewModel = new ListViewModel<Book>();
            DataContext = _viewModel;
        }

        private async void NextPageButtonClicked(object sender, RoutedEventArgs e)
        {
            DisablePaginationButtons();
            await _viewModel.FetchNextPage();
            EnablePaginationButtons();
        }

        private async void PreviousPageButtonClicked(object sender, RoutedEventArgs e)
        {
            DisablePaginationButtons();
            await _viewModel.FetchPreviousPage();
            EnablePaginationButtons();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void DisablePaginationButtons()
        {
            NextPageButton.IsEnabled = false;
            PreviousPageButton.IsEnabled = false;
        }

        private void EnablePaginationButtons()
        {
            NextPageButton.IsEnabled = true;
            PreviousPageButton.IsEnabled = true;
        }

        private void BookListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(BookDetailsPage), e.ClickedItem);
        }
    }
}
