using SYL_Mobile.DTO.User;
using SYL_Mobile.Models;
using SYL_Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SYL_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            if (email.Text == null || email.Text == "" || password.Text == null || password.Text == "")
            {
                await DisplayAlert("", "Please enter email and password", "Ok");
            }
            else
            {
                Users user = await App._userService.UserLogin(email.Text, password.Text);
                if (user != null)
                {
                    App.user = user;
                    App.userId = user.userId;
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("", "Email or password is incorrect!", "Ok");
                }
            }
        }

        private void RegisterClicked(object sender, EventArgs args)
        {
            //darbas su register
            App.Current.MainPage = new RegisterPage();
        }

    }
}