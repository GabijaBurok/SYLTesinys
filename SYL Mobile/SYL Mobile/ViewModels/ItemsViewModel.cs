using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using SYL_Mobile.Models;
using SYL_Mobile.Views;
using System.Collections.Generic;
using SYL_Mobile.Services;
using SYL.Mobile.Services;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading;

namespace SYL_Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products {
            get { return products; }
            set { SetProperty(ref products, value,"Products"); }
        }

        Dictionary<string, Position> adressCoordinates;

        public Command LoadItemsCommand { get;  }
        public Command AddItemCommand { get; }
        public Command ItemTapped { get; }

        private Product _selectedProd;
        public Product SelectedProduct {  
            get { return _selectedProd; } 
            set { if (_selectedProd != value) 
                    {
                    SetProperty(ref _selectedProd, value);
                    } 
            } 
        }

        public ItemsViewModel()
        {
            Title = "Products";
            Products = new ObservableCollection<Product>();

            adressCoordinates = new Dictionary<string, Position>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            AddItemCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new NewItemPage()));

            ItemTapped = new Command(async () => {
                if (SelectedProduct == null) return;
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProductPage((Product)SelectedProduct, adressCoordinates[SelectedProduct.adress]));
                    SelectedProduct = null;
                }                
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Location location;
            List<Product> products=new List<Product>();
            try
            {
                products = (List<Product>)await ProductService.GetProductsAsync(true);
                List<string> adressList = products.Select(x => x.adress).Distinct().ToList();

                var adr=new Dictionary<string, Position>();
                foreach (var adress in adressList) adr.Add(adress, await MapService.getCoordinates(adress));
                adressCoordinates = adr;


                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
                location = await Geolocation.GetLocationAsync(request, new CancellationTokenSource().Token);
            }
            catch (PermissionException e)
            {
                location = null;
                Debug.WriteLine(e);
            }
            catch (Exception ex)
            {
                location = null;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            Products.Clear();


            for (int i = 0; i < products.Count(); i++)
            {
                var product = products[i];

                var pos = adressCoordinates[product.adress];
                if (location == null) product.distance = 0;
                else product.distance = Math.Round(Distance.BetweenPositions(pos, new Position(location.Latitude, location.Longitude)).Kilometers, 2);
                product.imagePath = $"http://localhost:5001/images/{product.name.ToLower()}";

                if (i % 2 == 0) product.backgroundColor = Color.FromHex("#FFFFFF");
                else product.backgroundColor = Color.FromHex("#F0F0F0");

                Products.Add(product);

            }
            
        }

        public void OnAppearing()
        {            
            IsBusy = true;
        }


    }
}