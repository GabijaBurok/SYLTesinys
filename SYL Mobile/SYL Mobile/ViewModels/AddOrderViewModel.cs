using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using SYL_Mobile.Models;
using SYL_Mobile.Services;
using System.Text.Json;
using Trust_Your_Locals;
using Xamarin.Forms;
using System.Diagnostics;
using SYL_Mobile.Views;

namespace SYL_Mobile.ViewModels
{
    public class AddOrderViewModel : BaseViewModel
    {
      
        private string quantity;
        public Product product;

        public TimeSpan Time { get; set; }
        
        public AddOrderViewModel(Product product)
        {
            this.product = product;

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(quantity)
                && product != null;
        }

        public string Text
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();         
        }

        private async void OnSave()
        {
            int sID = await OrderService.getSellerId(product.sellerName);
            var order = new FormUrlEncodedContent(new[]
            {                
                new KeyValuePair<string, string>("name", product.name),
                new KeyValuePair<string, string>("time", Time.ToString().Substring(0, 5)),
                new KeyValuePair<string, string>("quantity", quantity),
                new KeyValuePair<string, string>("bID", 4.ToString()),   // user.getId
                new KeyValuePair<string, string>("sID", sID.ToString())
            });
            await OrderService.AddOrderAsync(order);

            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}
