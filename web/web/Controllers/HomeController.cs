using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFooService _fooService;

        public HomeController(IFooService fooService)
        {
            if (fooService == null)
                throw new ArgumentNullException("fooService");

            _fooService = fooService;
        }

        public ActionResult Index()
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
    }
}