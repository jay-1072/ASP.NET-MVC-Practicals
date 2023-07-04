using Practical12_Test3.Data;
using Practical12_Test3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12_Test3.Controllers
{
    public class DesignationController : Controller
    {
        private readonly Connection _conn = new Connection();

        [HttpGet]
        public ActionResult Index()
        {
            List<DesignationModel> designations = _conn.FetchDesignations();
            return View(designations);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DesignationModel model)
        {
            int rowAffected = _conn.AddDesignation(model);

            if(rowAffected != 0)
            {
                return RedirectToAction("Index", "Designation");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CreateStoreProcedureToAddDesignation()
        {
            bool result = _conn.CreateStoreProcedureToAddDesignation();
            if (result)
            {
                TempData["Success"] = "Procedure added successfully";
                return RedirectToAction("Index", "Designation");
            }
            return View("Error");
        }
    }
}