using Product.Management.Business.Repository.Concrete;
using Product.Management.Data.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Product.Management.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }
        protected void Application_Error()
        {
            ErrorRepository errorRepository = new ErrorRepository();
            var exception = Server.GetLastError(); //Oluşan hatayı değişkene atadık.
            LogInfo form = new LogInfo
            {
                Url = Request.Url.ToString(),
                Message = exception.Message,
                CreatedDateTime = DateTime.Now,
                IpAdress = HttpContext.Current.Request.UserHostName.ToString(),
            };
            var res = errorRepository.LogInfoAdd(form); //Log kaydını db ye ekledik
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError(); //Sunucudaki hatayı temizledik.
            Response.TrySkipIisCustomErrors = true; //IIS'in tipik hata sayfalarını görmezden geldik.
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error"; //Hata mesajlarını yöneteceğimiz Controller ismi
            routeData.Values["action"] = "Index"; //Controller içindeki default Action ismi
            routeData.Values["exception"] = exception;
            Response.StatusCode = httpException.GetHttpCode();
             
            IController errorsController = new Controllers.ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(rc);
        }
    }
}
