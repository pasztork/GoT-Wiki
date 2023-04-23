﻿using GoT_Wiki.Models;
using GoT_Wiki.ViewModels;
using GoT_Wiki.Views.DetailsViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GoT_Wiki.Views
{
    /// <summary>
    /// Used by View for showing HousesPage (list of houses).
    /// Contains all methods triggered by UI components.
    /// </summary>
    public sealed partial class HousesPage : Page
    {
        /// <summary>
        /// The ViewModel used by the Page.
        /// </summary>
        private readonly ListViewModel<House> _viewModel = null;

        /// <summary>
        /// Public ctor. Initializes component.
        /// Sets the data context to _viewModel.
        /// </summary>
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
