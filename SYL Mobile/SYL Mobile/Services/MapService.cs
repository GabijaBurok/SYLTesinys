using SYL_Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace SYL.Mobile.Services
{
    public static class MapService
    {
        public static async Task<Position > getCoordinates(string adress)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:5001/location?adress=" + adress;
            string response = await client.GetStringAsync(url);
            response=response.Trim(new char[] { '[', ']'});
            var list = (response.Split(',')).ToList();
            


            Position pos;
            if (list.Count == 2) pos = new Position(double.Parse(list[0]), double.Parse(list[1]));
            else pos = new Position(0, 0);

            return pos;

        }
    }
}
