 using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SYL_Mobile.ViewModels;
using SYL.Mobile.ViewModels;
using SYL_Mobile.Models;
using System.Diagnostics;

namespace SYL_Mobile.Views
{
    
    public partial class ItemsPage : ContentPage
    {
        
        ItemsViewModel _viewModel;
        public String returnedProduct="products: "; 
        
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();

            

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            

        }

        private void searchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchBar.Text.ToLower();
            var searchResult=_viewModel.Products.Where(c => (c.name.ToLower().Contains(text) || c.sellerName.ToLower().Contains(text)));
            ItemsListView.ItemsSource = searchResult;
        }

        private async void ShowMapClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MapPage(ItemsListView.ItemsSource, searchBar));
        }

        async void SortByClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Sort By:", "Cancel", null, "Name", "Seller Name", "Price", "Distance");
            Debug.WriteLine("Action: " + action);

            var list = new List<Product> ((IEnumerable<Product>)ItemsListView.ItemsSource);
            var newList = new List<Product>();
            if (action.Equals("Name")) newList = list.OrderBy(x => x.name).ToList();
            else if (action.Equals("Seller Name")) newList = list.OrderBy(x => x.sellerName).ToList();
            else if (action.Equals("Price")) newList = list.OrderBy(x => x.price).ToList();
            else if (action.Equals("Distance")) newList = list.OrderBy(x => x.distance).ToList();
            else newList = list;

            ItemsListView.ItemsSource = newList;



        }
        
    }
}