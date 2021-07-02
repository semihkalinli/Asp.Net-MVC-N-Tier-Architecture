using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.UI.Models;
using System;
using System.Web.Mvc;

namespace Product.Management.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }
        public ViewResult Index(Exception exception)
        {
            var res = new ErrorModel { ErrorTitle = "Bir Hata Meydana Geldi", ExceptionDetail = exception };
            return View(res);
        }

    }
}