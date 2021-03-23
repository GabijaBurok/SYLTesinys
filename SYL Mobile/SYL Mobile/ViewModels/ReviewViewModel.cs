using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using SYL_Mobile.Models;
using SYL_Mobile.Views;
using System.Collections.Generic;
using Trust_Your_Locals;
using SYL_Mobile.Services;

namespace SYL_Mobile.ViewModels
{
    class ReviewViewModel : BaseViewModel
{
        private Lazy<Review> _selectedReview;


        public ObservableCollection<Review> Reviews { get; set; }
        public Command LoadReviewsCommand { get; }
        public Command PlaceReviewCommand { get; set; }

        public string sellerName { get; set; }

        public ReviewViewModel(string sellerName)
        {
            this.sellerName = sellerName;
            Reviews = new ObservableCollection<Review>();
            LoadReviewsCommand = new Command(async () => await ExecuteLoadReviewsCommand());
            PlaceReviewCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new AddReviewPage(sellerName)));

        }


        async Task ExecuteLoadReviewsCommand()
        {
            IsBusy = true;
            try
            {
                var reviews = await ReviewService.GetReviewsAsync(sellerName, true);
                Reviews.Clear();
                foreach (var review in reviews) Reviews.Add(review);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        

        public void OnAppearing()
        {

            IsBusy = true;
            SelectedItem = null;
        }

        public Lazy<Review> SelectedItem
        {

            get => _selectedReview;
            set
            {
                SetProperty(ref _selectedReview, value);
            }
        }
    }
}
