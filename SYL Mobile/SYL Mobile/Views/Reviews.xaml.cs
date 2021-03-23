using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYL_Mobile.ViewModels;
using SYL_Mobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SYL_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Reviews : ContentPage
{
        ReviewViewModel _viewModel;
    public Reviews(Product product)
    {
        BindingContext = _viewModel = new ReviewViewModel(product.sellerName);
        InitializeComponent();
    }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}