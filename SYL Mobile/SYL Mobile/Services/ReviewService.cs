using SYL_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using SYL.Mobile.Services;

namespace SYL_Mobile.Services
{
    class ReviewService
{
        public static async Task<IEnumerable<Review>> loadReviews(string adress)
        {
            HttpClient client = new HttpClient();
            string url = "http://"+Secrets.IP+"/ratings/" + adress;
            string response = await client.GetStringAsync(url);
            var reviewList = JsonSerializer.Deserialize<List<Review>>(response);
            return reviewList;
        }
        public static async Task<Double> loadAvgReview(string adress)
        {
            HttpClient client = new HttpClient();
            string url = "http://"+Secrets.IP+"/ratings/" + adress + "/avg";
            string response = await client.GetStringAsync(url);
            return Convert.ToDouble(response);
        }
        

        public static async Task<bool> AddReviewAsync(FormUrlEncodedContent review, String sellerName)
        {
            var url = "http://"+Secrets.IP+"/ratings/" + sellerName + "/add";
            var client = new HttpClient();
            var response = await client.PostAsync(url, review);
            return await Task.FromResult(true);
        }

        public static async Task<IEnumerable<Review>> GetReviewsAsync(string url, bool forceRefresh = false )
        {
            return await loadReviews(url);
        }
        public static async Task<Double> GetAvgReviewAsync(string url, bool forceRefresh = false)
        {
            return await loadAvgReview(url);
        }
    }
}
