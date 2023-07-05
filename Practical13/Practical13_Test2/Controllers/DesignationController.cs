using Practical13_Test2.DAL;
using Practical13_Test2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Practical13_Test2.Controllers
{
    public class DesignationController : Controller
    {
        private DBContext _db = new DBContext();
        
        public ActionResult Index()
        {
            var designations = from d in _db.Designation select d;
            return View(designations);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DesignationModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _db.Designation.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Designation");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var designation = _db.Designation.Find(id);
            if(designation == null)
            {
                return HttpNotFound();
            }
            return View("Create", designation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, DesignationModel model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = _db.Designation.Find(id);
            if(TryUpdateModel(employeeToUpdate, "", new string[] { "Designation" }))
            {
                try
                {
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Designation");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again");
                }
            }
            return View("Create", employeeToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again";
            }
            var designation = _db.Designation.Find(id);
            if(designation==null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var designation = _db.Designation.Find(id);
                _db.Designation.Remove(designation);
                _db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", "Designation", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index", "Designation");
        }
    }
}
