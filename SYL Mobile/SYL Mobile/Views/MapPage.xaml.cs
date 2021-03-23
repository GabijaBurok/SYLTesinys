using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using SYL.Mobile.ViewModels;
using System.Collections;
using SYL_Mobile.Models;

namespace SYL_Mobile.Views {


public partial class MapPage : ContentPage
{
        MapViewModel mapModel;

        public MapPage(IEnumerable itemsSource, SearchBar searchBar)
        {
            InitializeComponent();
            BindingContext = mapModel = new MapViewModel(map, itemsSource, searchBar);
        }

        public MapPage()
        {
            InitializeComponent();
        }

    }
}