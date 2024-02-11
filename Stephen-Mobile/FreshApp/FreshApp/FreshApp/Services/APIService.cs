using FreshApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FreshApp.Services
{
    public class APIService
    {
        private HttpClient client;
        private string url = "http://10.131.73.238/api/values1/";

        public APIService()
        {
            client = new HttpClient();
            // client.DefaultRequestHeaders.Add("", "");
        }

        public async Task<bool> Login(string username, string password)
        {
            string url = this.url + $"Login?username={username}&password={password}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                App.User = JsonConvert.DeserializeObject<User>(result);

                var json = JsonConvert.SerializeObject(App.User);
                Preferences.Set("user", json);

                return true;
            }

            return false;
        }

        public async Task<bool> StoreItem(ItemDTO itemDTO)
        {
            string url = this.url + $"StoreItem?key={App.User.Key}";
            var json = JsonConvert.SerializeObject(itemDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<Captcha> GetCaptcha()
        {
            string url = this.url + $"GetCaptcha";
            var res = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<Captcha>(res);
            return result;
        }

        public async Task<List<string>> GetItemTypes()
        {
            string url = this.url + $"GetItemTypes";
            var res = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<List<string>>(res);
            return result;
        }

        public async Task<List<Item>> GetItems()
        {
            string url = this.url + $"GetItems?userId={App.User.Id}&key={App.User.Key}";
            var res = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<List<Item>>(res);
            return result;
        }

        public async Task<bool> UpdateItemStatus(int itemId)
        {
            string url = this.url + $"UpdateItemStatus?itemId={itemId}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<object>> GetObjects()
        {
            string url = this.url + $"GetObjects";
            var res = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<List<object>>(res);
            return result;
        }

        public async Task<bool> CheckObject(int id)
        {
            string url = this.url + $"CheckObject?id={id}";

            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> StoreObjects(object obj)
        {
            string url = this.url + $"StoreObjects";
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
