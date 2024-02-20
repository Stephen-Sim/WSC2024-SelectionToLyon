using FreshApi.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshApi.Tests
{
    [TestClass]
    public class FreshApiTest
    {
        [TestInitialize]
        public void Init()
        {
        
        }

        [TestMethod]
        public void Test()
        {
            int key = 14;

            var jsonStr = File.ReadAllText("data.txt");
            var json = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            int matched = 0, unmateched = 0;

            for (int i = 0; i < 30; i++)
            {
                if (CaesarCipher.Encrypt((json["data"][$"test{i + 1}"]["original"]).ToString(), key) == (json["data"][$"test{i + 1}"]["encrypted"]).ToString())
                {
                    matched++;
                }
                else
                {
                    unmateched++;
                }
            }

            Console.WriteLine($"Key Value: {key}");
            Console.WriteLine($"Matched: {matched}");
            Console.WriteLine($"Unmatched: {unmateched}");
        }

        [TestCleanup] public void Cleanup() 
        {
        
        }
    }
}
