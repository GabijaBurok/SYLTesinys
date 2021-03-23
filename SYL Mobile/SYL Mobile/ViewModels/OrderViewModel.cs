using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using SYL_Mobile.Models;
using SYL_Mobile.Views;
using System.Collections.Generic;
using Trust_Your_Locals;
using SYL_Mobile.Services;

namespace SYL_Mobile.ViewModels
{
    class OrderViewModel : BaseViewModel
{
        private Lazy<Order> _selectedOrder;

        public ObservableCollection<Order> Orders { get; set; }

        public Command LoadOrdersCommand { get; }       

        public OrderViewModel()
        {
            Orders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
        }


        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;
            try
            {
                var orders = await OrderService.GetOrdersAsync(true);
                Orders.Clear();
                foreach (var order in orders) Orders.Add(order);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {

            IsBusy = true;
            SelectedItem = null;
        }

        public Lazy<Order> SelectedItem
        {

            get => _selectedOrder;
            set
            {
                SetProperty(ref _selectedOrder, value);
            }
        }
    
}
}
