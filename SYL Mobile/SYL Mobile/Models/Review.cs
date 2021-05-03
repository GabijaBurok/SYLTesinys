using System;
using System.Collections.Generic;
using System.Text;

namespace SYL_Mobile.Models
{
    class Review
{
        public string reviewId { get; set; }
        public string customerId { get; set; }
        public string shopId { get; set; }
        public int reviewRating { get; set; }
        public string reviewComment { get; set; }
    }
}
