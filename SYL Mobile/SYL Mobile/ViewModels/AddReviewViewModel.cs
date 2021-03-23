using System;
using System.Collections.Generic;
using System.Text;
using SYL_Mobile.Models;
using SYL_Mobile.Services;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
using Syncfusion.SfRating.XForms;

namespace SYL_Mobile.ViewModels
{
    class AddReviewViewModel : BaseViewModel
{
        public string sellerName;
        public double rating;
        public string comment;

        public AddReviewViewModel(string sellerName)
        {
            this.sellerName = sellerName;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(comment)
                && rating != 0;
        }

        public string Text
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private async void OnSave()
        {
            var review = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "Lukas"),
                new KeyValuePair<string, string>("rating", rating.ToString()),
                new KeyValuePair<string, string>("text", comment),   
            });
            await ReviewService.AddReviewAsync(review, sellerName);
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}
