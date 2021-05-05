using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SYL_Mobile.Services;
using SYL_Mobile.Views;
using SYL_Mobile.Models;

namespace SYL_Mobile
{
    public partial class App : Application
    {
        public static readonly UserService _userService = new UserService();
        public static Users user { get; set; }
        public static string userId { get; set; }

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
