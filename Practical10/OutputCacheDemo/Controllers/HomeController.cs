using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutputCacheDemo.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 300)]
        public string Index()
        {
            return DateTime.Now.ToString();
        }
    }
}