using System;
using System.Collections.Generic;
using System.Text;

namespace SYL_Mobile.Models
{
    class Review
{
        public string username { get; set; }
        public double rating { get; set; }
        public string text { get; set; }
        public string sellerName { get; set; }
        public double avgRating { get; set; }
    }
}
