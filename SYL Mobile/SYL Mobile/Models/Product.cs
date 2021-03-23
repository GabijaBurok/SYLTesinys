using System;
using Xamarin.Forms;

namespace SYL_Mobile.Models
{
    public class Product
    {
        public string Id = Guid.NewGuid().ToString("N");
        public string name { get; set; }
        public string adress { get; set; }
        public double price { get; set; }
        public string sellerName { get; set; }

        public double distance { get; set; }

        public string imagePath { get; set; }

        public Color backgroundColor { get; set; }

    }
}