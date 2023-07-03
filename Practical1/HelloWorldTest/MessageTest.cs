using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practical1.Controllers;
using System;
using System.Web.Mvc;

namespace HelloWorldTest
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController homeController = new HomeController();

            ViewResult result = homeController.Index() as ViewResult;

            Assert.AreEqual("Hello World", result.ViewBag.Message);
        }
    }
}