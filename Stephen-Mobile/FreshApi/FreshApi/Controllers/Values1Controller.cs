using FreshApi.Helpers;
using FreshApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Globalization;

namespace FreshApi.Controllers
{
    public class Values1Controller : ApiController
    {
        public WSC2024Selection_Mobile_StephenEntities ent { get; set; }

        public Values1Controller()
        {
            ent = new WSC2024Selection_Mobile_StephenEntities();
        }

        [HttpGet]
        public async Task<object> Login(string username, string password)
        {
            var user = ent.users.FirstOrDefault(x => x.username == username && x.password == password);

            if (user == null)
            {
                return BadRequest();
            }

            // encrypted password to md5 + salt
            var encrytpedPass = BitConverter.ToString(HashAlgorithm.Create("md5").ComputeHash(Encoding.UTF8.GetBytes(user.salt + user.password))).Replace("-", "").ToLower();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("username", user.username);
                client.DefaultRequestHeaders.Add("password", encrytpedPass);

                var res = await client.PostAsync("http://localhost:5000/api/Logis/Login", null);

                var resString = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(resString);

                if ((int)result["status"] != 200)
                {
                    return BadRequest();
                }

                return Ok(new
                {
                    Id = user.id,
                    Key = Calculator.Calculate((result["key"]).ToString())
                });
            }
        }

        [HttpGet]
        public async Task<object> GetItemTypes()
        {
            var user = ent.users.First();

            int key = 0;

            // encrypted password to md5 + salt
            var encrytpedPass = BitConverter.ToString(HashAlgorithm.Create("md5").ComputeHash(Encoding.UTF8.GetBytes(user.salt + user.password))).Replace("-", "").ToLower();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("username", user.username);
                client.DefaultRequestHeaders.Add("password", encrytpedPass);

                var res = await client.PostAsync("http://localhost:5000/api/Logis/Login", null);

                var resString = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(resString);

                if ((int)result["status"] != 200)
                {
                    return BadRequest();
                }

                key = Calculator.Calculate((result["key"]).ToString());

                res = await client.PostAsync("http://localhost:5000/api/Logis/GetItemTypes", null);
                resString = await res.Content.ReadAsStringAsync();
                var result1 = JsonConvert.DeserializeObject<List<string>>(resString);

                for (int i = 0; i < result1.Count; i++)
                {
                    result1[i] = CaesarCipher.Decrypt(result1[i], 2);
                }

                return Ok(result1);
            }
        }

        [HttpGet]
        public object GetCaptcha()
        {
            var rand = new Random();
            string randomNum = rand.Next(1000, 9999).ToString();

            var bitmap = new Bitmap(200, 100);

            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            for (int j = 0; j < 5; j++)
            {
                Point[] curvePoints = new Point[5];
                for (int i = 0; i < curvePoints.Length; i++)
                {
                    curvePoints[i] = new Point(i * 50, rand.Next(10, 100));
                }
                graphics.DrawCurve(Pens.Gray, curvePoints);
            }

            for (int i = 0; i < randomNum.Length; i++)
            {
                var characterGraphics = Graphics.FromImage(bitmap);
                characterGraphics.TranslateTransform(i * 50 + 20, 50);
                characterGraphics.RotateTransform((i + 1) * 30);
                characterGraphics.DrawString(randomNum[i].ToString(), new Font("Arial", 12f), Brushes.Black, new Point(0, 0));
                characterGraphics.Dispose();
            }

            byte[] arr;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);

                arr = ms.ToArray();
            }

            return Ok(new { 
                Image = arr,
                Key = randomNum
            });
        }

        [HttpGet]
        public object GetItems(int key, int userId)
        {
            var items = ent.items.Where(x => x.userId == userId).ToList()
                .OrderBy(x => x.status)
                .ThenBy(x => DateTime.ParseExact(CaesarCipher.Decrypt(x.expiry_date, key), "dd MMMM yyyy", CultureInfo.InvariantCulture))
                .Select(x => new
                {
                    x.id,
                    name = CaesarCipher.Decrypt(x.name, key),
                    type = CaesarCipher.Decrypt(x.type, key),
                    address = CaesarCipher.Decrypt(x.address, key).Length >= 40 ? CaesarCipher.Decrypt(x.address, key).Substring(0, 37) + "..." : CaesarCipher.Decrypt(x.address, key).Substring(0, 27),
                    x.image,
                    expiry_date = DateTime.ParseExact(CaesarCipher.Decrypt(x.expiry_date, key), "dd MMMM yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                    x.status,
                    Color = new Func<string>(() =>
                    {
                        if (x.status)
                        {
                            return "purple";
                        }

                        int dayDiff = (DateTime.ParseExact(CaesarCipher.Decrypt(x.expiry_date, key), "dd MMMM yyyy", CultureInfo.InvariantCulture).Date - new DateTime(2024, 2, 27).Date).Days;

                        if (dayDiff <= 0)
                        {
                            return "red";
                        }

                        if (ent.dimdates.Any(y => y.day == dayDiff.ToString()))
                        {
                            return ent.dimdates.First(y => y.day == dayDiff.ToString()).color.Trim();
                        }

                        return "green";
                    })()
                });

            return Ok(items);
        }

        [HttpGet]
        public object UpdateItemStatus(int itemId)
        {
            try
            {
                var item = ent.items.FirstOrDefault(x => x.id == itemId);
                item.status = true;

                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public object StoreItem([FromBody] ItemDTO itemDTO, [FromUri]int key)
        {
            try
            {
                var item = new item
                {
                    status = false,
                    address = CaesarCipher.Encrypt(itemDTO.address, key),
                    expiry_date = CaesarCipher.Encrypt(itemDTO.expiry_date, key),
                    image = itemDTO.image,
                    name = CaesarCipher.Encrypt(itemDTO.name, key),
                    userId = itemDTO.userId,
                    type = CaesarCipher.Encrypt(itemDTO.type, key)
                };

                ent.items.Add(item);
                ent.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public class ItemDTO
        {
            public string name { get; set; }
            public string type { get; set; }
            public string address { get; set; }
            public byte[] image { get; set; }
            public string expiry_date { get; set; }
            public long userId { get; set; }
        }
    }
}
