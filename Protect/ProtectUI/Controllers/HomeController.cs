using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProtectUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Knowledge()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Getweather()
        {
            cn.com.webxml.www.WeatherWebService w = new cn.com.webxml.www.WeatherWebService();
            string[] s = new string[23];

            s = w.getWeatherbyCityName("南昌");
            ViewData.Model = s;
            return PartialView(s);

        }
    }
}