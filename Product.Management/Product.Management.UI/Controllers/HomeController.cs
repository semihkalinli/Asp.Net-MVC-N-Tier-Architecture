using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Product.Management.UI.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "OneMinute")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

    }
}