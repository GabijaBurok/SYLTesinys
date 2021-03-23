using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class OrderAddPage : ContentPage
    {
        public OrderAddPage(Product product)
        {
            InitializeComponent();
            BindingContext = new AddOrderViewModel(product);
            _label.Text += product.name;
        }

        //private void _timePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    BindingContext = new AddOrderViewModel(product, _timePicker);
        //}
    }
}