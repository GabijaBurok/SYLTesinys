using SYL_Mobile.Models;
using SYL_Mobile.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using SYL_Mobile.Services;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace SYL_Mobile.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {

        public string Price { get; set; }

        public string sellerName { get; set; }

        public string productName { get; set; }

        public double avg;
        public double Avg
        {
            get { return avg; }
            set { SetProperty(ref avg, value); }
        }

        private string distance;
        public string Distance {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        public Position position { get; set; }

        public Command PlaceOrderCommand { get; set; }

        public Command CheckReviewCommand { get; set; }

        public Command PlaceReviewCommand { get; set; }

        public Command ShowAvgRating { get; set; }

        public ProductViewModel(Product p, Position pos)
        {
            GetAvgReview(p.sellerName);
            position = pos;
            Price = p.price + " €/kg";
            Distance = p.distance + " km";
            sellerName = p.sellerName;
            productName = p.name;

            PlaceOrderCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new OrderAddPage(p)));
            CheckReviewCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new Views.Reviews(p)));

            var timer = new Timer((e) => UpdateDistance(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5)); 
        }
        async void GetAvgReview(string url)
        {
            Avg = await ReviewService.GetAvgReviewAsync(url);
        }
        
        async void UpdateDistance()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request, new CancellationTokenSource().Token);
                var curPos = new Position(location.Latitude, location.Longitude);
                Distance = Convert.ToString(Math.Round(Xamarin.Forms.Maps.Distance.BetweenPositions(position, curPos).Kilometers, 2)) + " km";
            }
            catch (PermissionException)
            {
                Distance = "Unable to get distance.";
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }


            
        }







    }
}
