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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void RegisterClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AppShell();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AppShell();
        }
    }
}