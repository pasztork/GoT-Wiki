using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoT_Wiki.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BooksPage : Page
    {
        public BooksPage()
        {
            this.InitializeComponent();
        }

        private async void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            DisablePaginationButtons();
            await ViewModel.FetchNextPage();
            EnablePaginationButtons();
        }

        private async void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            DisablePaginationButtons();
            await ViewModel.FetchPreviousPage();
            EnablePaginationButtons();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
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
    }
}
