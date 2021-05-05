using System;
using System.Collections.Generic;
using System.Text;

namespace SYL_Mobile.DTO.Review
{
    class ShopReviewDTO
    {
        public string customerName { get; set; }
        public string shopName { get; set; }
        public int reviewRating { get; set; }
        public string reviewComment { get; set; }
    }
}
