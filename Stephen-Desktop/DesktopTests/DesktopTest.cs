using Desktop;
using Desktop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTests
{
    [TestClass]
    public class DesktopTest
    {
        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void Test()
        {
            WSC2024Selection_Desktop_StephenEntities ent = new WSC2024Selection_Desktop_StephenEntities();

            Graph g = new Graph(ent.routes.Count() * 2);

            var routes = ent.routes.ToList();

            // add all edges
            foreach (var route in routes)
            {
                g.AddEdge(route.location.id, route.location1.id);
                g.AddEdge(route.location1.id, route.location.id);
            }

            var jsonStr = File.ReadAllText("data.txt");
            var json = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            var data = json["data"];

            int matched = 0, unmatched = 0;

            foreach (var item in data)
            {
                List<List<long>> paths = g.FindAllPaths((long)item["departure_location_id"], (long)item["arrival_location_id"]);
                long maxDuration = 0;

                foreach (List<long> path in paths)
                {
                    var day = 0;

                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        var route = ent.routes.ToList().FirstOrDefault(x => (x.location1Id == path[i] && x.location2Id == path[i + 1])
                        || (x.location1Id == path[i + 1] && x.location2Id == path[i]));

                        if (route != null)
                        {
                            day += route.duration;
                        }
                    }

                    if (day >= maxDuration)
                    {
                        maxDuration = day;
                    }
                }

                if (maxDuration == (long)item["longest_duration"])
                {
                    matched++;
                }
                else
                {
                    unmatched++;
                }
            }

            Console.WriteLine($"Matched: {matched}");
            Console.WriteLine($"Unmatched: {unmatched}");
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
