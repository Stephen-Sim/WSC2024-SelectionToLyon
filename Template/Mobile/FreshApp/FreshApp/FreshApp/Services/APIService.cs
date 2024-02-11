using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FreshApp.Services
{
    public class APIService
    {
        private HttpClient client;
        private string url = "http://10.105.13.82:45455/api/get/";

        public APIService()
        {
            client = new HttpClient();

            // client.DefaultRequestHeaders.Add("", "");
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
