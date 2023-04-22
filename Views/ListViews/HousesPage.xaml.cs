using GoT_Wiki.Models;
using GoT_Wiki.ViewModels;
using GoT_Wiki.Views.DetailsViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GoT_Wiki.Views
{
    public sealed partial class HousesPage : Page
    {
        private readonly ListViewModel<House> _viewModel = null;

        public HousesPage()
        {
            InitializeComponent();
            _viewModel = new ListViewModel<House>();
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

        private void HouseListItemClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }
    }
}
