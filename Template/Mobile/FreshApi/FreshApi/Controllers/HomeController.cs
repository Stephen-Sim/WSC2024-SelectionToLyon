using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public FileResult downloadExcel()
        {
            byte[] data = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    /*GetController get = new GetController();
                    dynamic result = get.GetRows(quizId, attemptTypeId, isExpertOnly);

                    sw.WriteLine("Name,Attempt,Completion,Grade,Published Status");

                    foreach (var row in result)
                    {
                        sw.WriteLine("{0},{1},{2},{3},{4}", ((string)row.User).Replace("\n", " "), row.Attempt, row.Completion, row.Grade, row.DateTime);
                    }*/
                }

                data = ms.ToArray();
            }

            return File(data, "text/csv", "result.csv");
        }
    }
}
