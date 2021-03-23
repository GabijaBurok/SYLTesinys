using System;
using System.Collections.Generic;
using SYL_Mobile.ViewModels;
using SYL_Mobile.Views;
using Xamarin.Forms;

namespace SYL_Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
