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

namespace SYL_Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        public List<string> ProductEnum { get; }

        public Picker picker { get; }



        public NewItemViewModel(Picker p)
        {
            picker = p;

            ProductEnum = new List<string>(Enum.GetNames(typeof(ProductEnum)));

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

           
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && picker.SelectedItem != null;
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

       
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var product = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("shopID", 23.ToString()),   // user.getId
                new KeyValuePair<string, string>("price", text),
                new KeyValuePair<string, string>("name", picker.SelectedItem.ToString()),
                new KeyValuePair<string, string>("pID", (picker.SelectedIndex+1).ToString())
            });

            await ProductService.AddProductAsync(product);
           
            await Shell.Current.GoToAsync("..");
        }
    }
}
