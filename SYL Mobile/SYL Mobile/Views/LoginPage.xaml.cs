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

        private void LoginClicked(object sender, EventArgs e)
        { 
            //darbas su login
            App.Current.MainPage = new AppShell();
        }

        private void RegisterClicked(object sender, EventArgs args)
        {
            //darbas su register
            App.Current.MainPage = new RegisterPage();
        }

    }
}