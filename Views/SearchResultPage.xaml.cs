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
    public sealed partial class SearchResultPage : Page
    {
        public SearchResultPage()
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var queryString = e.Parameter as string;
            await ViewModel.Load(queryString);
        }

        private void BooksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(BookDetailsPage), e.ClickedItem);
        }

        private void CharactersList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(CharacterDetailsPage), e.ClickedItem);
        }

        private void HousesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }
    }
}
