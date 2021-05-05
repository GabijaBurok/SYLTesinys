using System;
using System.Collections.Generic;
using System.Text;
using SYL_Mobile.Models;
using SYL_Mobile.Services;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
using Syncfusion.SfRating.XForms;
using SYL_Mobile.DTO.Review;

namespace SYL_Mobile.ViewModels
{
    class AddReviewViewModel : BaseViewModel
{
        public string shopName;
        public int rating;
        public string comment;

        public AddReviewViewModel(string shopName)
        {
            this.shopName = shopName;
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
            var review = new NewReviewDTO
            {
                customerId = App.userId,
                customerName = App.user.userName + " " + App.user.userLastName,
                shopName = shopName,
                reviewRating = rating,
                reviewComment = comment
            };
            await ReviewService.AddReviewAsync(review);
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}
