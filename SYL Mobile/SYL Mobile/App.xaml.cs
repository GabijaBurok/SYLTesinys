using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SYL_Mobile.Services;
using SYL_Mobile.Views;

namespace SYL_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
           
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
