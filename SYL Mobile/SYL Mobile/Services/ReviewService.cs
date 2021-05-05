using SYL_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using SYL.Mobile.Services;
using SYL_Mobile.DTO.Review;
using Newtonsoft.Json;

namespace SYL_Mobile.Services
{
    class ReviewService
{
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> AddReviewAsync(NewReviewDTO review)
        {
            var json = JsonConvert.SerializeObject(review);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:5001/api/review";

            try
            {
                var response = await client.PostAsync(url, httpContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
            return false;
        }

        public static async Task<IEnumerable<ShopReviewDTO>> GetReviewsAsync(string seller, bool forceRefresh = false )
        {
            try
            {
                string url = "http://localhost:5001/api/review/GetReviewsByShop/" + seller;
                var response = await client.GetAsync(String.Format(url));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<ShopReviewDTO> reviews = JsonConvert.DeserializeObject<List<ShopReviewDTO>>(responseBody);
                    if (reviews != null)
                    {
                        return reviews;
                    }
                }
            }
            catch (Exception )
            { }

            return new List<ShopReviewDTO>();

        }
        public static async Task<Double> GetAvgReviewAsync(string seller, bool forceRefresh = false)
        {

            try
            {
                string url = "http://localhost:5000/api/review/avg/" + seller;
                var response = await client.GetAsync(String.Format(url));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var rating = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (rating != null)
                    {
                        return Convert.ToDouble(rating);
                    }
                }
            }
            catch (Exception)
            { }

            return 0;
        }
    }
}
