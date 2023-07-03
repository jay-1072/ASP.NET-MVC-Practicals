using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExceptionFilterDemo.Controllers
{
    public class HomeController : Controller
    {
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "DivideByZero")]
        public ActionResult Index()
        {
            try
            {
                int a = 10;
                int b = 0;

                int res = 10 / b;
            }
            catch(Exception ex)
            {
                return View("DivideByZero");
            }

            return View();
        }
    }
}