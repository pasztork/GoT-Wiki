﻿using GoT_Wiki.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoT_Wiki.Views.DetailsViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HouseDetailsPage : Page
    {
        public HouseDetailsPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var house = e.Parameter as House;
            _ = ViewModel.Load(house);
        }

        private void House_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Frame.Navigate(typeof(HouseDetailsPage), button.Tag);
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Frame.Navigate(typeof(CharacterDetailsPage), button.Tag);
        }

        private void HouseList_Click(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(HouseDetailsPage), e.ClickedItem);
        }
    }
}
