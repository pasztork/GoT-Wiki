using Windows.UI.Xaml.Controls;

namespace GoT_Wiki.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BooksButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BooksPage));
        }

        private void CharactersButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CharactersPage));
        }

        private void HousesButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HousesPage));
        }

        private void SearchBarQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.QueryText))
            {
                return;
            }
            Frame.Navigate(typeof(SearchResultPage), args.QueryText);
        }
    }
}
