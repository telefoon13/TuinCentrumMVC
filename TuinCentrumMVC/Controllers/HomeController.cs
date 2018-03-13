using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TuinCentrumMVC.Controllers
{
    [RoutePrefix("Thuis")]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        //Alleen via /Thuis/Over niet via /Home/About
        [Route("Over")]
        public ActionResult About()
        {
            return View();
        }

        //Alleen via /Home/Contact niet via Thuis
        [Route("~/Home/Contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}