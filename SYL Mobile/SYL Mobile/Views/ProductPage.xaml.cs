using SYL.Mobile.Services;
using SYL_Mobile.Models;
using SYL_Mobile.ViewModels;
using System.IO;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace SYL_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        ProductViewModel context;
        
        public ProductPage(Product product, Position position)
        {
            BindingContext = context = new ProductViewModel(product, position);
            InitializeComponent();

            var path = $"http://localhost:5001/images/{product.name.ToLower()}";
            image.Source = ImageSource.FromUri(new System.Uri(path));
        }
    }
}