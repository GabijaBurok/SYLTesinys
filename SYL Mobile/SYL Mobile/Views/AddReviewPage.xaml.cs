using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYL_Mobile.Models;

using SYL_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SYL_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReviewPage : ContentPage
    {
           
            AddReviewViewModel reviewView;
        
        public AddReviewPage(string sellerName)
        {
            BindingContext = reviewView = new AddReviewViewModel(sellerName);
            InitializeComponent();
            _label.Text += sellerName;
        
        }

        private void rating_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            reviewView.rating=rating.Value;
        }
    }
}