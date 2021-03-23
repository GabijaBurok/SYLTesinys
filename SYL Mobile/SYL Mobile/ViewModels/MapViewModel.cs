using SYL.Mobile.Services;
using SYL_Mobile;
using SYL_Mobile.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SYL.Mobile.ViewModels
{
	public class MapViewModel : ContentView
{
        private IEnumerable<Product> currentProducts;

        private Xamarin.Forms.Maps.Map map;

        private SearchBar sB;

        public MapViewModel(Xamarin.Forms.Maps.Map map, IEnumerable itemsSource, SearchBar searchBar)
        {
            this.map = map;
            sB = searchBar;
            currentProducts = (IEnumerable<Product>)itemsSource;

            ExecuteLoadPinsCommand();

        }

        async void ExecuteLoadPinsCommand()
        {
            List<string> adressList = currentProducts.Select(x => x.adress).Distinct().ToList();

            map.Pins.Clear();
            foreach (var adress in adressList)
            {
                map.Pins.Add(new Pin { 
                    Label= currentProducts.Where(x => x.adress == adress).Select(x => x.sellerName).First(),
                    Address=adress,
                    Type=PinType.Place,
                    Position=await MapService.getCoordinates(adress)

                });
            }

            foreach (Pin pin in map.Pins)
            {
                pin.InfoWindowClicked += async (obj, args) =>
                {
                    sB.Text = ((Pin)obj).Label;
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                };
            }

            double south = map.Pins.Min(pin => pin.Position.Latitude);
            double north = map.Pins.Max(pin => pin.Position.Latitude);
            double west = map.Pins.Min(pin => pin.Position.Longitude);
            double east = map.Pins.Max(pin => pin.Position.Longitude);

            Position center = new Position((south + north) / 2, (west + east) / 2);

            Distance latidudinal = Distance.BetweenPositions(new Position(north, center.Longitude), new Position(south, center.Longitude));
            Distance longitudinal = Distance.BetweenPositions(new Position(center.Latitude, west), new Position(center.Latitude, east));


            Distance radius = (latidudinal.Kilometers > longitudinal.Kilometers) ? latidudinal : longitudinal;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(center, radius));
            

        }
    }
}