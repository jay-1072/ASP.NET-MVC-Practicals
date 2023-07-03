using Practical10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Practical10.Controllers
{
    [RoutePrefix("Result")]
    public class ResultController : Controller
    {
        // GET: Result

        [Route("Index")]
        public JsonResult Index()
        {
            var people = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Jay", LastName = "Koshti"},
                new Person() { Id = 2, FirstName = "Harry", LastName = "Potter"},
                new Person() { Id = 3, FirstName = "John", LastName = "Due"},
            };

            var jsonResult = Json(data: people, contentType: "application/json", contentEncoding: System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [Route("Empty")]
        public EmptyResult Empty()
        {
            return new EmptyResult();
        }

        public ActionResult NothingReturned()
        {
            return null;
        }

        [ActionName(name: "Content")]
        public ContentResult ContentResultDemo()
        {
            return Content("<h3>This text has heading style h3.</h3>", "text/html", System.Text.Encoding.UTF8);
        }

        public ActionResult JSResultDemo()
        {
            return View();
        }

        public JavaScriptResult WarningMessage()
        {
            var msg = "alert('Are you sure want to Continue?');";
            return new JavaScriptResult() { Script = msg };
        }

        public FileContentResult FileResultDemo()
        {
            var x = HostingEnvironment.ApplicationPhysicalPath;

            string fileContent = System.IO.File.ReadAllText($"{x}/Data/TextFile1.txt");
            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent);
            string contentType = "text/xml";
            return new FileContentResult(fileBytes, contentType) { FileDownloadName = "Hello.txt" };
        }
    }
}