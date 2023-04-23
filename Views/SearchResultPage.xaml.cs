using GoT_Wiki.Views.DetailsViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GoT_Wiki.Views
{
    /// <summary>
    /// Used by View for showing search result on SearchResultPage.
    /// Contains all methods triggered by UI components.
    /// </summary>
    public sealed partial class SearchResultPage : Page
    {
        /// <summary>
        /// Public ctor. Initializes component.
        /// </summary>
        public SearchResultPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var queryString = e.Parameter as string;
            await ViewModel.Load(queryString);
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void BookListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(BookDetailsPage), e.ClickedItem);
        }

        private void CharacterListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(CharacterDetailsPage), e.ClickedItem);
        }

        private void HouseListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }
    }
}
