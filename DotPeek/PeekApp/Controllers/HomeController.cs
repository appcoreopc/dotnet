using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeekApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IKernel kernel = new StandardKernel();
            var samurai = kernel.Get<Samurai>();
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
    }
}